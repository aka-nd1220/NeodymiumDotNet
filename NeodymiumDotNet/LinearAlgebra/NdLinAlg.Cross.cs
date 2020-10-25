using System;
using System.Collections.Generic;
using Op = NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {

        private static readonly IndexArray _CrossAvailableShape = new[] { 3, };


        /// <summary>
        ///     Evaluates cross operation of 3-dim vector lazily.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static NdArray<T> Cross<T>(this INdArray<T> x, INdArray<T> y)
        {
            Guard.AssertShapeMatch(new[] { 3 }, x.Shape, nameof(x) + "." + nameof(INdArray<T>.Shape));
            Guard.AssertShapeMatch(new[] { 3 }, y.Shape, nameof(y) + "." + nameof(INdArray<T>.Shape));

            var p = Op.Subtract(Op.Multiply(x[1], y[2]), Op.Multiply(x[2], y[1]));
            var q = Op.Subtract(Op.Multiply(x[2], y[0]), Op.Multiply(x[0], y[2]));
            var r = Op.Subtract(Op.Multiply(x[0], y[1]), Op.Multiply(x[1], y[0]));
            return NdArray.Create(new[] { p, q, r });
        }
    }
}
