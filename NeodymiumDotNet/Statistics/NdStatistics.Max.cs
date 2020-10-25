using System;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {
        /// <summary>
        ///     Returns the largest value of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static T Max<T>(this INdArray<T> ndArray)
        {
            var len = ndArray.Shape.TotalLength;
            Guard.AssertOperation(len > 0, "ndArray has no elements.");

            var max = ndArray.GetItem(0);
            for(var i = 1; i < len; ++i)
            {
                var current = ndArray.GetItem(i);
                if(ValueTrait.GreaterThan(current, max))
                    max = current;
            }
            return max;
        }

        /// <summary>
        ///     Returns the index of the largest value of the <paramref name="ndArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static IndexArray ArgMax<T>(this INdArray<T> ndArray)
        {
            var len = ndArray.Shape.TotalLength;
            Guard.AssertOperation(len > 0, "ndArray has no elements.");

            var argmax = 0;
            var max = ndArray.GetItem(0);
            for(var i = 1; i < len; ++i)
            {
                var current = ndArray.GetItem(i);
                if(ValueTrait.GreaterThan(current, max))
                {
                    argmax = i;
                    max = current;
                }
            }
            return ndArray.ToShapedIndices(argmax);
        }

    }
}
