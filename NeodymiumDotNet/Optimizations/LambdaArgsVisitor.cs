using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    /// <summary>
    /// An expression tree visitor to replace parameters of lambda.
    /// </summary>
    public sealed class LambdaArgsVisitor : ExpressionVisitor
    {
        /// <summary>
        ///     Gets a replacement pairs from parameters to other expressions.
        /// </summary>
        public IReadOnlyDictionary<ParameterExpression, Expression> ArgsReplacementPairs { get; }


        /// <summary>
        ///     Creates a new instance.
        /// </summary>
        /// <param name="argsReplacementPairs"></param>
        public LambdaArgsVisitor(IReadOnlyDictionary<ParameterExpression, Expression> argsReplacementPairs)
        {
            ArgsReplacementPairs = argsReplacementPairs;
        }


        /// <summary>
        ///     Visits a parameter expression to return its replaced expression.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
            => ArgsReplacementPairs.TryGetValue(node, out var value)
               ? value
               : base.VisitParameter(node);
    }
}
