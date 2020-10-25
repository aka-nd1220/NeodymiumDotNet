using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace NeodymiumDotNet.Optimizations.Test
{
    public class ExpressionComparerTest
    {
        public static IEnumerable<object[]> TestArgs()
        {
            object[] core<TDelegate>(Expression<TDelegate> x, Expression<TDelegate> y, bool expected)
                => new object[] { x, y, expected };

            yield return core<Func<object>>(() => null, () => null, true);
            yield return core<Func<object>>(() => null, () => 0, false);
            yield return core<Func<object>>(() => 0, () => 0, true);
            yield return core<Func<int, int>>(x => x, x => x, true);
            yield return core<Func<int, int>>(x => x + 1, x => x + 1, true);
            yield return core<Func<int, int>>(x => x, x => 0, false);
            yield return core<Func<int, int>>(x => x, y => y, false);
            yield return core<Func<double, double, double>>((x, y) => Math.Max(x, y), (x, y) => Math.Max(x, y), true);
        }


        [Theory]
        [MemberData(nameof(TestArgs))]
        public void TestGetHashCode(Expression x, Expression y, bool expected)
        {
            Assert.Equal(expected, ExpressionComparer.Instance.GetHashCode(x) == ExpressionComparer.Instance.GetHashCode(y));
        }


        [Theory]
        [MemberData(nameof(TestArgs))]
        public void TestEquals(Expression x, Expression y, bool expected)
        {
            Assert.Equal(expected, ExpressionComparer.Instance.Equals(x, y));
        }
    }
}
