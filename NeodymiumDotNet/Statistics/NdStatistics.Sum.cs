using System;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {
        /// <summary>
        ///     Computes sum from all elements of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T Sum<T>(this INdArray<T> ndArray)
        {
            var value = ValueTrait.Zero<T>();
            var len = ndArray.Shape.TotalLength;
            for(var i = 0; i < len; ++i)
                value = ValueTrait.Add(value, ndArray.GetItem(i));
            return value;
        }

    }
}
