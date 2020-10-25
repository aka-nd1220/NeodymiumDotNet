using System;
using System.Collections.Generic;
using System.Text;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     Solves simultaneous linear equations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"> [<c>a.Rank == 2 &amp;&amp; a.Shape[0] == a.Shape[1]</c>] </param>
        /// <param name="b"> [<c>(b.Rank == 1 || b.Rank == 2) &amp;&amp; b.Shape[0] == a.Shape[0]</c>] </param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException">
        ///     <para> When <c>n := a.Shape[0]</c>, </para>
        ///     <para> - If <c>b.Shape == {n}</c>, then <c>$ReturnValue.Shape == {n}</c>. </para>
        ///     <para>
        ///         - If <c>b.Shape == {n, p}</c>, then <c>$ReturnValue.Shape == {n, p}</c>.
        ///         Each column of return value is the solution against corresponding column of b.
        ///     </para>
        /// </exception>
        public static NdArray<T> Solve<T>(this INdArray<T> a, INdArray<T> b)
        {
            Guard.AssertShapeMatch(a.Rank == 2 && a.Shape[0] == a.Shape[1], "a.Rank == 2 && a.Shape[0] == a.Shape[1]");
            if(b.Rank == 1 && b.Shape[0] == a.Shape[0])
            {
                var (l, u, perms) = a.LUWithPermutationsLegacy();
                var x = NdArray.CreateMutable(new T[b.Shape[0]]);
                SolveCore(l, u, perms, b, x);
                return x.MoveToImmutable();
            }
            if(b.Rank == 2 && b.Shape[0] == a.Shape[0])
            {
                var (l, u, perms) = a.LUWithPermutationsLegacy();
                var x = NdArray.CreateMutable(new T[b.Shape[0], b.Shape[1]]);
                var col = b.Shape[1];
                for(var j = 0; j < col; ++j)
                {
                    var bj = b[Range.Whole, new Index(j, false)];
                    var xj = x[Range.Whole, new Index(j, false)];
                    SolveCore(l, u, perms, bj, xj);
                }
                return x.MoveToImmutable();
            }

            Guard.ThrowShapeMismatch("(b.Rank == 1 || b.Rank == 2) && b.Shape[0] == a.Shape[0]");
            throw new NotSupportedException();
        }



        private static void SolveCore<T>(INdArray<T> l, INdArray<T> u, IReadOnlyList<(int, int)> perms, INdArray<T> b, MutableNdArray<T> x)
        {
            var dim = b.Shape[0];
            var zz = b.ToMutable();
            var z = zz.Entity;
            for(var i = perms.Count - 1; i >= 0; --i)
            {
                var (p, q) = perms[i];
                InternalUtils.Exchange(ref z[p], ref z[q]);
            }
            var y = new T[dim];
            for(var i = 0; i < dim;++i)
            {
                y[i] = z[i];
                for(var j = 0; j < i; ++j)
                    y[i] = Subtract(y[i], Multiply(l[i, j], y[j]));
            }
            for(var i = dim - 1; i >= 0; --i)
            {
                x[i] = y[i];
                for(var j = dim - 1; j > i; --j)
                    x[i] = Subtract(x[i], Multiply(u[i, j], x[j]));
                x[i] = Divide(x[i], u[i, i]);
            }
        }
    }
}
