using System;
using NeodymiumDotNet.Statistics;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     [Pure] Returns the 1-D norm of all elements of <paramref name="ndArray"/>.
        ///     This is same with <see cref="NdStatistics.Sum"/>.
        /// </summary>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T Norm1<T>(this INdArray<T> ndArray)
            => ndArray.Sum();


        /// <summary>
        ///     [Pure] Returns the 2-D norm of all elements of <paramref name="ndArray"/>.
        /// </summary>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T Norm2<T>(this INdArray<T> ndArray)
        {
            var len = ndArray.Shape.TotalLength;
            Guard.AssertOperation(len > 0, "ndArray has no elements.");

            var norm = ValueTrait.Zero<T>();
            for(var i = 0; i < len; ++i)
            {
                var element = ndArray.GetItem(i);
                norm = ValueTrait.Add(norm, ValueTrait.Multiply(element, element));
            }

            return NdMath.Sqrt(norm);
        }


        /// <summary>
        ///     [Pure] Returns the Infinity-D norm of all elements of <paramref name="ndArray"/>.
        ///     This is same with <see cref="NdStatistics.Max"/>.
        /// </summary>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T NormInf<T>(this INdArray<T> ndArray)
            => ndArray.Max();
    }
}