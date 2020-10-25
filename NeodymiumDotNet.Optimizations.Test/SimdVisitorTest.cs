using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using Xunit;

namespace NeodymiumDotNet.Optimizations.Test
{
    public partial class SimdVisitorTest
    {
        public static Vector<T> GetVector<T>()
            where T : unmanaged
        {
            Span<T> span = stackalloc T[Vector<T>.Count];
            var x = ValueTrait.UnaryNegate(ValueTrait.One<T>());
            for(var i = 0; i < Vector<T>.Count; ++i)
            {
                span[i] = x;
                x = ValueTrait.Increment(x);
            }
            return new Vector<T>(span);
        }


        [Fact]
        public void VisitFuncLambda()
        {
            TestCore(() => 0);
            TestCore(x => x, GetVector<int>());
            TestCore((x, y) => x + y, GetVector<int>(), GetVector<int>());
            TestCore((x, y) => x - y, GetVector<int>(), GetVector<int>());
            TestCore((x, y) => x * y, GetVector<int>(), GetVector<int>());
            TestCore((x, y, z) => x + y + z, GetVector<int>(), GetVector<int>(), GetVector<int>());
            TestCore((x, y, z) => x - y - z, GetVector<int>(), GetVector<int>(), GetVector<int>());
            TestCore((x, y, z) => x * y * z, GetVector<int>(), GetVector<int>(), GetVector<int>());

            TestCore(x => Math.Abs(x), GetVector<double>());
            TestCore((x, y) => Math.Min(x, y), GetVector<double>(), GetVector<double>());
            TestCore((x, y) => Math.Max(x, y), GetVector<double>(), GetVector<double>());
            TestCore(x => Math.Sqrt(x), GetVector<double>());

            TestCore((x, y, z, u) => Math.Sqrt(x + y) + Math.Sqrt(z + u), GetVector<double>(), GetVector<double>(), GetVector<double>(), GetVector<double>());
        }
    }
}
