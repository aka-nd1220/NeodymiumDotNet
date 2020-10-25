using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    /// <summary>
    /// An expression tree visitor to convert primitive value lambda operation to SIMD.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed partial class SimdVisitor<T> : ExpressionVisitor
        where T : unmanaged
    {
        private readonly bool _UsesSpecialMethod;
        private readonly Dictionary<ParameterExpression, ParameterExpression> _paramReplacementSet;


        /// <summary>
        ///     Gets a set of new parameters.
        /// </summary>
        public IEnumerable<ParameterExpression> NewArguments => _paramReplacementSet.Values;


        /// <summary>
        /// Creates a new instance to simdize a specified lamda.
        /// </summary>
        /// <param name="func">
        /// A lambda expression to simdize.
        /// </param>
        /// <param name="usesSpecialMethod">
        /// Tries to use specially implemented SIMD method in the evaluation of specified lambda if <c>true</c>; otherwise does not.
        /// If <c>true</c> the calculation performance will be faster but the result may change in some case.
        /// </param>
        public SimdVisitor(LambdaExpression func, bool usesSpecialMethod = true)
        {
            OptGuard._SimdVisitor<T>.ThrowIfArgumentsMismatch(func);

            _UsesSpecialMethod = usesSpecialMethod;
            _paramReplacementSet = func.Parameters.ToDictionary(
                p => p,
                p => Expression.Parameter(typeof(Vector<T>), p.Name)
                );
        }


        /// <inheritdoc />
        public override Expression Visit(Expression node)
        {
            OptGuard._SimdVisitor<T>.ThrowIfNodeTypeMismatch(node);
            return base.Visit(node);
        }


        /// <inheritdoc />
        protected override Expression VisitConstant(ConstantExpression node)
            => Expression.New(MemberTable._Vector<T>.ScaleConstructor, node);


        /// <inheritdoc />
        protected override Expression VisitParameter(ParameterExpression node)
            => _paramReplacementSet.TryGetValue(node, out var value)
               ? value
               : base.VisitParameter(node);


        /// <inheritdoc />
        protected override Expression VisitBinary(BinaryExpression node)
            => Expression.MakeBinary(node.NodeType, Visit(node.Left), Visit(node.Right));


        /// <inheritdoc />
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            OptGuard._SimdVisitor<T>.ThrowIfInstanceMethodCalled(node);

            if(_UsesSpecialMethod)
            {
                var methodCallBuilder = SimdVisitorSpecialMethod<T>.GetSpecialMethod(node);
                if(methodCallBuilder != null)
                    return methodCallBuilder(node.Arguments.Select(Visit));
            }
            return VisitMethodCallCore(node);
        }


        private Expression VisitMethodCallCore(MethodCallExpression node)
        {
            var i = Expression.Parameter(typeof(int), "i");
            var retval = Expression.Parameter(typeof(T[]), "retval");
            var arguments = node.Arguments.Select((_, i) => Expression.Parameter(typeof(Vector<T>), $"_{i}")).ToArray();
            var vectorCount = Expression.Property(null, MemberTable._Vector<T>.Count);

            var block = new List<Expression>();
            block.Add(Expression.Assign(
                retval,
                Expression.NewArrayBounds(typeof(T), vectorCount)
                ));
            block.AddRange(node.Arguments.Zip(arguments, (arge, argv) =>
                Expression.Assign(argv, Visit(arge))
                ));
            block.Add(ExpressionEx.For(
                Expression.Assign(i, Expression.Constant(0)),
                Expression.LessThan(i, vectorCount),
                Expression.PreIncrementAssign(i),
                Expression.Assign(
                    Expression.ArrayAccess(retval, i),
                    Expression.Call(
                        node.Object,
                        node.Method,
                        arguments.Select(arg => Expression.Property(arg, MemberTable._Vector<T>.Item, i))
                        )
                    )
                ));
            block.Add(Expression.New(MemberTable._Vector<T>.ArrayConstructor, retval));

            return Expression.Block(
                new ParameterExpression[] { i, retval }.Concat(arguments),
                block
                );
        }

    }
}
