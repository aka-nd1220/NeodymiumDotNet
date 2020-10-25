using System;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {
        /// <summary>
        ///     Returns the smallest value of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T Min<T>(this INdArray<T> ndArray)
        {
            var len = ndArray.Shape.TotalLength;
            Guard.AssertOperation(len > 0, "ndArray has no elements.");

            var min = ndArray.GetItem(0);
            for(var i = 1; i < len; ++i)
            {
                var current = ndArray.GetItem(i);
                if(ValueTrait.LessThan(current, min))
                    min = current;
            }
            return min;
        }

        /// <summary>
        ///     Returns the index of the smallest value of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static IndexArray ArgMin<T>(this INdArray<T> ndArray)
        {
            var len = ndArray.Shape.TotalLength;
            Guard.AssertOperation(len > 0, "ndArray has no elements.");

            var argmin = 0;
            var min = ndArray.GetItem(0);
            for(var i = 1; i < len; ++i)
            {
                var current = ndArray.GetItem(i);
                if(ValueTrait.LessThan(current, min))
                {
                    argmin = i;
                    min = current;
                }
            }
            return ndArray.ToShapedIndices(argmin);
        }

    }
}
