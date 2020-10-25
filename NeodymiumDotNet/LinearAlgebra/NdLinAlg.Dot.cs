using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Optimizations;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     Evaluates dot operation of vector/matrix lazily.
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
        public static NdArray<T> Dot<T>(this NdArray<T> x, NdArray<T> y)
        {
            var xEntity = x.GetOrCopyBuffer();
            switch(y.Rank)
            {
            case 1:
                var yEntity = y.GetOrCopyBuffer();
                switch(x.Rank)
                {
                case 1:
                    return Dot1x1(xEntity, yEntity);
                case 2:
                    return Dot2x1(xEntity, yEntity);
                default:
                    break;
                }
                break;
            case 2:
                var yTEntity = y.Transpose().ToMutable().GetOrCopyBuffer();
                switch(x.Rank)
                {
                case 1:
                    return Dot1x2(xEntity, yTEntity);
                case 2:
                    return Dot2x2(xEntity, yTEntity);
                default:
                    break;
                }
                break;
            default:
                break;
            }
            Guard.ThrowShapeMismatch($"The ranks of x and y must be 1 or 2. (x.Rank={x.Rank}, y.Rank={y.Rank})");
            throw new NotSupportedException();
        }


        private static NdArray<T> Dot1x1<T>(IBufferNdArrayImpl<T> x, IBufferNdArrayImpl<T> y)
        {
            // x.Shape = [p]
            // y.Shape = [p]
            // retval.Shape = [1]
            var p = x.Shape[0];
            Guard.AssertShapeMatch(p == y.Shape[0], $"x.Shape[0] = {p}, y.Shape[0] = {y.Shape[0]}");

            var resImpl = new RawNdArrayImpl<T>(new IndexArray(1));
            var xBuff = x.Buffer;
            var yBuff = y.Buffer;
            var resBuff = resImpl.Buffer.Span;
            resBuff[0] = VectorOperation.Dot(xBuff, yBuff);
            return new NdArray<T>(resImpl);
        }


        private static NdArray<T> Dot1x2<T>(IBufferNdArrayImpl<T> x, IBufferNdArrayImpl<T> yT)
        {
            // x.Shape = [p]
            // yT.Shape = [n, p]
            // retval.Shape = [n]
            var p = x.Shape[0];
            Guard.AssertShapeMatch(p == yT.Shape[1], $"x.Shape[0] = {p}, y.Shape[0] = {yT.Shape[1]}");
            var n = yT.Shape[0];

            var resImpl = new RawNdArrayImpl<T>(new IndexArray(n));
            var xBuff = x.Buffer;
            var yTBuff = yT.Buffer;
            var resBuff = resImpl.Buffer.Span;

            for(var j = 0; j < n; ++j)
                resBuff[j] = VectorOperation.Dot(xBuff, yTBuff.Slice(j * p, p));

            return new NdArray<T>(resImpl);
        }


        private static NdArray<T> Dot2x1<T>(IBufferNdArrayImpl<T> x, IBufferNdArrayImpl<T> y)
        {
            // x.Shape = [m, p]
            // y.Shape = [p]
            // retval.Shape = [m]
            var p = x.Shape[1];
            Guard.AssertShapeMatch(p == y.Shape[0], $"x.Shape[1] = {p}, y.Shape[0] = {y.Shape[0]}");
            var m = x.Shape[0];

            var resImpl = new RawNdArrayImpl<T>(new IndexArray(m));
            var xBuff = x.Buffer;
            var yBuff = y.Buffer;
            var resBuff = resImpl.Buffer.Span;

            for(var i = 0; i < m; ++i)
                resBuff[i] = VectorOperation.Dot(xBuff.Slice(i * p, p), yBuff);

            return new NdArray<T>(resImpl);
        }


        private static NdArray<T> Dot2x2<T>(IBufferNdArrayImpl<T> x, IBufferNdArrayImpl<T> yT)
        {
            // x .Shape = [m, p]
            // yT.Shape = [n, p]
            // retval.Shape = [m, n]
            var p = x.Shape[1];
            Guard.AssertShapeMatch(
                p == yT.Shape[1],
                $"x.Shape[1] = {p}, y.Shape[0] = {yT.Shape[1]}");
            var m = x.Shape[0];
            var n = yT.Shape[0];

            var resImpl = new RawNdArrayImpl<T>(new IndexArray(m, n));
            var xBuff = x.Buffer;
            var yTBuff = yT.Buffer;
            var resBuff = resImpl.Buffer.Span;

            for(var i = 0; i < m; ++i)
                for(var j = 0; j < n; ++j)
                    resBuff[n * i + j] = VectorOperation.Dot(xBuff.Slice(i * p, p), yTBuff.Slice(j * p, p));

            return new NdArray<T>(resImpl);
        }
    }
}
