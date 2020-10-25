using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text;
using Expr = System.Linq.Expressions.Expression;


namespace NeodymiumDotNet.Optimizations
{
    internal static class SimdVisitorSpecialMethod<T>
        where T : unmanaged
    {
        private static readonly Dictionary<MethodInfo, Func<IEnumerable<Expr>, MethodCallExpression>> _replacementPairs;


        static SimdVisitorSpecialMethod()
        {
            _replacementPairs = new Dictionary<MethodInfo, Func<IEnumerable<Expr>, MethodCallExpression>>();
            if(MemberTable._Math.Max<T>.Method is { } max)
                _replacementPairs.Add(max, exprs => Expr.Call(null, MemberTable._Vector.Max<T>.Method, exprs));
            if(MemberTable._Math.Min<T>.Method is { } min)
                _replacementPairs.Add(min, exprs => Expr.Call(null, MemberTable._Vector.Min<T>.Method, exprs));
            if(MemberTable._Math.Sqrt<T>.Method is { } sqrt)
                _replacementPairs.Add(sqrt, exprs => Expr.Call(null, MemberTable._Vector.Sqrt<T>.Method, exprs));
        }


        public static Func<IEnumerable<Expr>, MethodCallExpression>? GetSpecialMethod(MethodCallExpression node)
        {
            if(_replacementPairs.TryGetValue(node.Method, out var result))
                return result;
            return null;
        }
    }
}
