using System;
using System.Collections.Generic;
using System.Linq;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     [Legacy] Evaluates dot operation of vector/matrix lazily.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"> [<c>x.Rank == 1 || x.Rank == 2</c>] </param>
        /// <param name="y"> [<c>y.Rank == 1 || y.Rank == 2</c>] </param>
        /// <returns>
        ///     <para> The result of dot operation of <paramref name="x"/> and <paramref name="y"/>. </para>
        ///     <para> - If <c>x.Shape == {p} &amp;&amp; y.Rank == {p}</c>, then <c>$ReturnValue.Shape == {1}</c>. </para>
        ///     <para> - If <c>x.Shape == {p} &amp;&amp; y.Shape == {p, n}</c>, then <c>$ReturnValue.Shape == {n}</c>. </para>
        ///     <para> - If <c>x.Shape == {m, p} &amp;&amp; y.Shape == {p}</c>, then <c>$ReturnValue.Shape == {m}</c>. </para>
        ///     <para> - If <c>x.Shape == {m, p} &amp;&amp; y.Shape == {p, n}</c>, then <c>$ReturnValue.Shape == {m, n}</c>. </para>
        ///     <para> - If the shape patterns does not match with above patterns, throw <see cref="ShapeMismatchException"/>. </para>
        /// </returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static NdArray<T> DotLegacy<T>(this INdArray<T> x, INdArray<T> y)
        {
            if(x.Rank == 1)
            {
                if(y.Rank == 1)
                    return DotLegacy1x1(x, y);
                if(y.Rank == 2)
                    return DotLegacy1x2(x, y);
            }
            else if(x.Rank == 2)
            {
                if(y.Rank == 1)
                    return DotLegacy2x1(x, y);
                if(y.Rank == 2)
                    return DotLegacy2x2(x, y);
            }

            Guard.ThrowShapeMismatch($"The ranks of x and y must be 1 or 2. (x.Rank={x.Rank}, y.Rank={y.Rank})");
            throw new NotSupportedException();
        }


        private static NdArray<T> DotLegacy1x1<T>(INdArray<T> x, INdArray<T> y)
        {
            // x.Shape = [p]
            // y.Shape = [p]
            // retval.Shape = [1]
            var p = x.Shape[0];
            Guard.AssertShapeMatch(p == y.Shape[0], $"x.Shape[0] = {p}, y.Shape[0] = {y.Shape[0]}");

            var rawImpl = new RawNdArrayImpl<T>(new IndexArray(1));
            var buffer = rawImpl.Buffer;
            buffer.Span[0] = Zero<T>();
            for(var i = 0; i < p; ++i)
                buffer.Span[0] = Add(buffer.Span[0], Multiply(x[i], y[i]));

            return new NdArray<T>(rawImpl);
        }


        private static NdArray<T> DotLegacy1x2<T>(INdArray<T> x, INdArray<T> y)
        {
            // x.Shape = [p]
            // y.Shape = [p, n]
            // retval.Shape = [n]
            var p = x.Shape[0];
            var n = y.Shape[1];
            Guard.AssertShapeMatch(p == y.Shape[0], $"x.Shape[0] = {p}, y.Shape[0] = {y.Shape[0]}");

            var rawImpl = new RawNdArrayImpl<T>(new IndexArray(n));
            var buffer = rawImpl.Buffer;
            for(var j = 0; j < n; ++j)
            {
                buffer.Span[j] = Zero<T>();
                for(var k = 0; k < p; ++k)
                    buffer.Span[j] = Add(buffer.Span[j], Multiply(x[k], y[k, j]));
            }

            return new NdArray<T>(rawImpl);
        }


        private static NdArray<T> DotLegacy2x1<T>(INdArray<T> x, INdArray<T> y)
        {
            // x.Shape = [m, p]
            // y.Shape = [p]
            // retval.Shape = [m]
            var p = x.Shape[1];
            var m = x.Shape[0];
            Guard.AssertShapeMatch(p == y.Shape[0], $"x.Shape[1] = {p}, y.Shape[0] = {y.Shape[0]}");

            var rawImpl = new RawNdArrayImpl<T>(new IndexArray(m));
            var buffer = rawImpl.Buffer;
            for(var i = 0; i < m; ++i)
            {
                buffer.Span[i] = Zero<T>();
                for(var k = 0; k < p; ++k)
                    buffer.Span[i] = Add(buffer.Span[i], Multiply(x[i, k], y[k]));
            }

            return new NdArray<T>(rawImpl);
        }


        private static NdArray<T> DotLegacy2x2<T>(INdArray<T> x, INdArray<T> y)
        {
            // x.Shape = [m, p]
            // y.Shape = [p, n]
            // retval.Shape = [m, n]
            var p = x.Shape[1];
            var m = x.Shape[0];
            var n = y.Shape[1];
            Guard.AssertShapeMatch(p == y.Shape[0], $"x.Shape[1] = {p}, y.Shape[0] = {y.Shape[0]}");

            var rawImpl = new RawNdArrayImpl<T>(new IndexArray(m, n));
            var buffer = rawImpl.Buffer;
            for(var i = 0; i < m; ++i)
                for(var j = 0; j < n; ++j)
                {
                    buffer.Span[n * i + j] = Zero<T>();
                    for(var k = 0; k < p; ++k)
                        buffer.Span[n * i + j] = Add(buffer.Span[n * i + j], Multiply(x[i, k], y[k, j]));
                }

            return new NdArray<T>(rawImpl);
        }

    }
}
