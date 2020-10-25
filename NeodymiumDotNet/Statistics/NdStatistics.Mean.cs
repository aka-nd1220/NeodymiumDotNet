using System;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {
        /// <summary>
        ///     Computes mean from all elements of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T Mean<T>(this INdArray<T> ndArray)
            => ValueTrait.Divide(ndArray.Sum(), ValueTrait.FromLong<T>(ndArray.Shape.TotalLength));

    }
}
