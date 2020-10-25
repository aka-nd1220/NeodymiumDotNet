using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using Expr = System.Linq.Expressions.Expression;

namespace NeodymiumDotNet.Optimizations
{
    partial class VectorOperation
    {
        private static readonly ConcurrentDictionary<LambdaExpression, Delegate> _cache
            = new ConcurrentDictionary<LambdaExpression, Delegate>(ExpressionComparer.Instance);


        private static TDelegate Simdize<T, TDelegate>(LambdaExpression expr)
            where T : unmanaged
            where TDelegate : Delegate
        {
            if(_cache.TryGetValue(expr, out var value))
                return (TDelegate)value;

            var simdVisitor = new SimdVisitor<T>(expr);
            var simdBody = simdVisitor.Visit(expr.Body);

            var i = Expr.Parameter(typeof(int), "i");
            var len = Expr.Parameter(typeof(int), "len");

            var xMemories = expr.Parameters.Select(p => Expr.Parameter(typeof(ReadOnlyMemory<T>), p.Name)).ToArray();
            var resultMemory = Expr.Parameter(typeof(Memory<T>), "result");

            var xSpans = xMemories.Select(p => Expr.Variable(typeof(ReadOnlySpan<T>), $"{p.Name}Span")).ToArray();
            var resultSpan = Expr.Variable(typeof(Span<T>), "resultSpan");

            var xVecSpans = xMemories.Select(p => Expr.Variable(typeof(ReadOnlySpan<Vector<T>>), $"{p.Name}VecSpan")).ToArray();
            var resultVecSpan = Expr.Variable(typeof(Span<Vector<T>>), "resultVecSpan");

            var xSpanGetters = xSpans.Select(p => Expr.Call(null, MemberTable._ReadOnlySpan<T>.GetItem, p, i)).ToArray();
            var xVecSpanGetters = xVecSpans.Select(p => Expr.Call(null, MemberTable._ReadOnlySpan<Vector<T>>.GetItem, p, i)).ToArray();

            var exprCall = new LambdaArgsVisitor(expr.Parameters.Zip(xSpanGetters).ToDictionary(tpl => tpl.Item1, tpl => (Expr)tpl.Item2)).Visit(expr.Body);
            var simdCall = new LambdaArgsVisitor(simdVisitor.NewArguments.Zip(xVecSpanGetters).ToDictionary(tpl => tpl.Item1, tpl => (Expr)tpl.Item2)).Visit(simdBody);

            var resultSpanSetter = Expr.Call(null, MemberTable._Span<T>.SetItem, resultSpan, i, exprCall);
            var resultVecSpanSetter = Expr.Call(null, MemberTable._Span<Vector<T>>.SetItem, resultVecSpan, i, simdCall);

            var parameters = xMemories.Concat(new[] { resultMemory });
            var variables = xSpans.Concat(xVecSpans).Concat(new[] { i, len, resultSpan, resultVecSpan });

            var block = new List<Expr>();

            //  xSpan = xMemory.Span;
            block.AddRange(xMemories.Zip(xSpans, (memory, span) =>
                Expr.Assign(
                    span,
                    Expr.Property(memory, MemberTable._ReadOnlyMemory<T>.Span)
                    )
                ));

            //  xVecSpan = MemoryMarshal.Cast<T, Vector<T>>(xSpan);
            block.AddRange(xSpans.Zip(xVecSpans, (span, vSpan) =>
                Expr.Assign(
                    vSpan,
                    Expr.Call(null, MemberTable._MemoryMarshal.Cast<T, Vector<T>>.ForReadOnlySpan, span)
                    )
                ));

            //  resultSpan = resultMemory.Span;
            block.Add(
                Expr.Assign(
                    resultSpan,
                    Expr.Property(resultMemory, MemberTable._Memory<T>.Span)
                    )
                );

            //  resultVecSpan = MemoryMarshal.Cast<T, Vector<T>>(resultSpan);
            block.Add(
                Expr.Assign(
                    resultVecSpan,
                    Expr.Call(null, MemberTable._MemoryMarshal.Cast<T, Vector<T>>.ForSpan, resultSpan)
                    )
                );

            //  for(i = 0, len = resultVecSpan.Length & ~0b1111; i < len; )
            //  {
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x0
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x1
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x2
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x3
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x4
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x5
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x6
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x7
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x8
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x9
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0xA
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0xB
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0xC
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0xD
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0xE
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0xF
            //  }
            block.Add(
                ExpressionEx.For(
                    Expr.Block(
                        Expr.Assign(i, Expr.Constant(0)),
                        Expr.Assign(
                            len,
                            Expr.And(Expr.Property(resultVecSpan, MemberTable._Span<Vector<T>>.Length), Expr.Constant(~0b1111))
                            )
                        ),
                    Expr.LessThan(i, len),
                    Expr.Empty(),
                    Expr.Block(
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x0
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x1
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x2
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x3
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x4
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x5
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x6
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x7
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x8
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x9
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0xA
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0xB
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0xC
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0xD
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0xE
                        resultVecSpanSetter, Expr.PreIncrementAssign(i)   // 0xF
                        )
                    )
                );

            //  if(i < (resultVecSpan.Length & ~0b111))
            //  {
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x0
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x1
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x2
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x3
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x4
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x5
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x6
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x7
            //  }
            block.Add(
                Expr.IfThen(
                    Expr.LessThan(
                        i,
                        Expr.And(Expr.Property(resultVecSpan, MemberTable._Span<Vector<T>>.Length), Expr.Constant(~0b111))
                        ),
                    Expr.Block(
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x0
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x1
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x2
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x3
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x4
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x5
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x6
                        resultVecSpanSetter, Expr.PreIncrementAssign(i)   // 0x7
                        )
                    )
                );

            //  if(i < (resultVecSpan.Length & ~0b11))
            //  {
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x0
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x1
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x2
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x3
            //  }
            block.Add(
                Expr.IfThen(
                    Expr.LessThan(
                        i,
                        Expr.And(Expr.Property(resultVecSpan, MemberTable._Span<Vector<T>>.Length), Expr.Constant(~0b11))
                        ),
                    Expr.Block(
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x0
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x1
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x2
                        resultVecSpanSetter, Expr.PreIncrementAssign(i)   // 0x3
                        )
                    )
                );

            //  if(i < (resultVecSpan.Length & ~0b1))
            //  {
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x0
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x1
            //  }
            block.Add(
                Expr.IfThen(
                    Expr.LessThan(
                        i,
                        Expr.And(Expr.Property(resultVecSpan, MemberTable._Span<Vector<T>>.Length), Expr.Constant(~0b1))
                        ),
                    Expr.Block(
                        resultVecSpanSetter, Expr.PreIncrementAssign(i),  // 0x0
                        resultVecSpanSetter, Expr.PreIncrementAssign(i)   // 0x1
                        )
                    )
                );

            //  if(i < resultVecSpan.Length)
            //  {
            //      resultVecSpan[i] = simdCall(xVecSpan[i], ...); ++i;  // 0x0
            //  }
            block.Add(
                Expr.IfThen(
                    Expr.LessThan(
                        i,
                        Expr.Property(resultVecSpan, MemberTable._Span<Vector<T>>.Length)
                        ),
                    Expr.Block(
                        resultVecSpanSetter, Expr.PreIncrementAssign(i)   // 0x0
                        )
                    )
                );

            //  for(i = Vector<T>.Count * resultVecSpan.Length; i < resultSpan.Length; )
            //  {
            //      resultSpan[i] = exprCall(xSpan[i], ...); ++i;
            //  }
            block.Add(
                ExpressionEx.For(
                    Expr.Assign(
                        i,
                        Expr.Multiply(
                            Expr.Constant(Vector<T>.Count),
                            Expr.Property(resultVecSpan, MemberTable._Span<Vector<T>>.Length)
                            )
                        ),
                    Expr.LessThan(i, Expr.Property(resultSpan, MemberTable._Span<T>.Length)),
                    Expr.Empty(),
                    Expr.Block(
                        resultSpanSetter, Expr.PreIncrementAssign(i)
                        )
                    )
                );
            var retval = Expr.Lambda<TDelegate>(Expr.Block(variables, block), parameters).Compile();
            _cache.TryAdd(expr, retval);
            return retval;
        }
    }
}
