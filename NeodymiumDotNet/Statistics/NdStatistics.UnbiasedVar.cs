using System;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {
        /// <summary>
        ///     Computes unbiased variance from all elements of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T UnbiasedVar<T>(this INdArray<T> ndArray)
        {
            var value = ValueTrait.Zero<T>();
            var mean = ndArray.Mean();
            var len = ndArray.Shape.TotalLength;
            for(var i = 0; i < len; ++i)
            {
                var temp = ValueTrait.Subtract(ndArray.GetItem(i), mean);
                value = ValueTrait.Multiply(temp, temp);
            }
            return ValueTrait.Divide(value, ValueTrait.FromLong<T>(len - 1));
        }

    }
}
