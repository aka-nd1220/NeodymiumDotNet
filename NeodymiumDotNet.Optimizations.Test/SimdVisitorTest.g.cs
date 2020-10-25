using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using Xunit;
namespace NeodymiumDotNet.Optimizations.Test
{
    partial class SimdVisitorTest
    {
        private void TestCore<T>(
            Expression<Func<T>> expr
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f();
                expected = new Vector<T>(buffer);
            }
            var actual = sf();

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T>> expr
            , Vector<T> x1
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected}\nactual: {s_actual}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    var text = Vector.Max(x1, x2).ToString();
                    Console.WriteLine(text);
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected}\nactual: {d_actual}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            , Vector<T> x3
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i], x3[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2, x3);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            , Vector<T> x3
            , Vector<T> x4
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i], x3[i], x4[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2, x3, x4);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T, T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            , Vector<T> x3
            , Vector<T> x4
            , Vector<T> x5
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i], x3[i], x4[i], x5[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2, x3, x4, x5);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T, T, T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            , Vector<T> x3
            , Vector<T> x4
            , Vector<T> x5
            , Vector<T> x6
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2, x3, x4, x5, x6);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T, T, T, T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            , Vector<T> x3
            , Vector<T> x4
            , Vector<T> x5
            , Vector<T> x6
            , Vector<T> x7
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i], x7[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2, x3, x4, x5, x6, x7);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

        private void TestCore<T>(
            Expression<Func<T, T, T, T, T, T, T, T, T>> expr
            , Vector<T> x1
            , Vector<T> x2
            , Vector<T> x3
            , Vector<T> x4
            , Vector<T> x5
            , Vector<T> x6
            , Vector<T> x7
            , Vector<T> x8
            )
            where T : unmanaged
        {
            var visitor = new SimdVisitor<T>(expr);
            var simd_expr = visitor.VisitFuncLambda(expr);
            var f = expr.Compile();
            var sf = simd_expr.Compile();
            Vector<T> expected;
            {
                var buffer = new T[Vector<T>.Count];
                for(var i = 0; i < Vector<T>.Count; ++i)
                    buffer[i] = f(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i], x7[i], x8[i]);
                expected = new Vector<T>(buffer);
            }
            var actual = sf(x1, x2, x3, x4, x5, x6, x7, x8);

            if(typeof(T) == typeof(float))
            {
                var s_expected = (Vector<float>)expected;
                var s_actual = (Vector<float>)actual;
                for(var i = 0; i < Vector<float>.Count; ++i)
                {
                    Assert.False(
                        float.IsNaN(s_expected[i]) ^ float.IsNaN(s_actual[i]),
                        $"expected: {s_expected[i]}, actual: {s_actual[i]}");
                    if(float.IsNaN(s_expected[i]))
                        continue;
                    Assert.Equal(s_expected[i], s_actual[i]);
                }
            }
            else if(typeof(T) == typeof(double))
            {
                var d_expected = (Vector<double>)expected;
                var d_actual = (Vector<double>)actual;
                for(var i = 0; i < Vector<double>.Count; ++i)
                {
                    Assert.False(
                        double.IsNaN(d_expected[i]) ^ double.IsNaN(d_actual[i]),
                        $"expected: {d_expected[i]}, actual: {d_actual[i]}");
                    if(double.IsNaN(d_expected[i]))
                        continue;
                    Assert.Equal(d_expected[i], d_actual[i]);
                }
            }
            else
                Assert.Equal(expected, actual);
        }

    }
}
