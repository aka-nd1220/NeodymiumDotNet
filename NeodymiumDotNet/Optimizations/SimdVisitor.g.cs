using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

namespace NeodymiumDotNet.Optimizations
{
    partial class SimdVisitor<T>
    {
        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>>>
               VisitFuncLambda(Expression<Func<T>> func)
            => Expression.Lambda<Func<Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T, T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T, T, T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T, T, T, T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


        /// <summary>
        ///     Visits the children of the operation expression for primitive value.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Expression<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>
               VisitFuncLambda(Expression<Func<T, T, T, T, T, T, T, T, T>> func)
            => Expression.Lambda<Func<Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>, Vector<T>>>(Visit(func.Body), NewArguments);


    }
}
