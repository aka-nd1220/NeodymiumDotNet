using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    internal static class OptGuard
    {
        internal static class _SimdVisitor<T>
            where T : unmanaged
        {
            internal static void ThrowIfArgumentsMismatch(LambdaExpression func)
            {
                if(func.Parameters.Any(p => p.Type != typeof(T)) || func.Body.Type != typeof(T))
                    throw new ArgumentException($"{nameof(SimdVisitor<T>)} requires same type for returns, all parameters, and all calculation processes.");
            }


            internal static void ThrowIfNodeTypeMismatch(Expression node)
            {
                if(node.Type != typeof(T))
                    throw new ArgumentException("All evaluation results in a simdizing expression must be a target type.");
            }


            internal static void ThrowIfInstanceMethodCalled(MethodCallExpression node)
            {
                if(node.Object != null)
                    throw new ArgumentException("A simdizing expression cannot contain instance method.");
            }
        }
    }
}
