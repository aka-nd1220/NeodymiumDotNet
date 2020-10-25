using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    /// <summary>
    ///     Provides utilities for <see cref="Expression"/>.
    /// </summary>
    public static class ExpressionEx
    {
        /// <summary>
        ///     Gets a method caller expression of <see cref="Console.WriteLine(Object)"/>.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Expression ConsoleWriteLine(Expression arg)
            => Expression.Call(null, MemberTable._Console.WriteLineObject, Expression.Convert(arg, typeof(object)));


        /// <summary>
        ///     Creates a <see cref="BlockExpression"/> that represents an for-loop.
        /// </summary>
        /// <param name="initialize"></param>
        /// <param name="condition"></param>
        /// <param name="iterate"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public static BlockExpression For(
            Expression initialize,
            Expression condition,
            Expression iterate,
            params Expression[] block)
            => For(initialize, condition, iterate, Expression.Block(block));


        /// <summary>
        ///     Creates a <see cref="BlockExpression"/> that represents an for-loop.
        /// </summary>
        /// <param name="initialize"></param>
        /// <param name="condition"></param>
        /// <param name="iterate"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public static BlockExpression For(
            Expression initialize,
            Expression condition,
            Expression iterate,
            IEnumerable<Expression> block)
            => For(initialize, condition, iterate, Expression.Block(block));


        /// <summary>
        ///     Creates a <see cref="BlockExpression"/> that represents an for-loop.
        /// </summary>
        /// <param name="initialize"></param>
        /// <param name="condition"></param>
        /// <param name="iterate"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public static BlockExpression For(
            Expression initialize,
            Expression condition,
            Expression iterate,
            BlockExpression block
            )
        {
            initialize = initialize ?? Expression.Empty();
            condition = condition ?? Expression.Constant(true);
            iterate = iterate ?? Expression.Empty();
            var breakTarget = Expression.Label();
            return Expression.Block(
                initialize,
                Expression.Loop(
                    Expression.Block(
                        Expression.IfThen(Expression.Not(condition), Expression.Break(breakTarget)),
                        block,
                        iterate
                        ),
                    breakTarget
                    )
                );
        }


        /// <summary>
        ///     Creates a <see cref="BlockExpression"/> that represents an for-loop.
        /// </summary>
        /// <param name="initialize"></param>
        /// <param name="condition"></param>
        /// <param name="iterate"></param>
        /// <param name="blockBuilder"></param>
        /// <returns></returns>
        public static BlockExpression For(
            Expression initialize,
            Expression condition,
            Expression iterate,
            Func<LabelTarget, BlockExpression> blockBuilder
            )
        {
            initialize = initialize ?? Expression.Empty();
            condition = condition ?? Expression.Constant(true);
            iterate = iterate ?? Expression.Empty();
            var breakTarget = Expression.Label();
            return Expression.Block(
                initialize,
                Expression.Loop(
                    Expression.Block(
                        Expression.IfThen(Expression.Not(condition), Expression.Break(breakTarget)),
                        blockBuilder(breakTarget),
                        iterate
                        ),
                    breakTarget
                    )
                );
        }
    }
}
