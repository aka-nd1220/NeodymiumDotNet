using System;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {
        /// <summary>
        ///     Computes sample variance from all elements of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T StdDev<T>(this INdArray<T> ndArray)
            => NdMath.Sqrt(ndArray.SampleVar());

    }
}
