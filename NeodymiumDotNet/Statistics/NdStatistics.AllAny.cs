using System;
using System.Collections.Generic;
using System.Linq;

namespace NeodymiumDotNet.Statistics
{
    partial class NdStatistics
    {

        #region All

        /// <summary>
        ///     Determines whether all elements of a NdArray is <c>true</c>.
        /// </summary>
        /// <param name="ndarray"> An <see cref="NdArray{T}"/> whose elements to determine. </param>
        /// <returns>
        ///     <c>true</c> if all elements of the NdArray is <c>true</c>;
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool All(this NdArray<bool> ndarray)
        {
            var len    = ndarray.Length;
            var entity = ndarray.Entity;
            for(var i = 0 ; i < len ; ++i)
                if(!entity[i])
                    return false;
            return true;
        }


        /// <summary>
        ///     Determines whether all elements of a NdArray satisfies a condition.
        /// </summary>
        /// <param name="ndarray"> An <see cref="NdArray{T}"/> whose elements to apply the predicate to. </param>
        /// <param name="predicate"> A function to test all elements for a condition. </param>
        /// <returns>
        ///     <c>true</c> if all elements in the NdArray pass the test in the specified predicate;
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool All<T>(this NdArray<T> ndarray, Func<T, bool> predicate)
        {
            var len    = ndarray.Length;
            var entity = ndarray.Entity;
            for(var i = 0 ; i < len ; ++i)
                if(!predicate(entity[i]))
                    return false;
            return true;
        }

        #endregion

        #region Any

        /// <summary>
        ///     Determines whether any element of a NdArray is <c>true</c>.
        /// </summary>
        /// <param name="ndarray"> An <see cref="NdArray{T}"/> whose elements to determine. </param>
        /// <returns>
        ///     <c>true</c> if any element of the NdArray is <c>true</c>;
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool Any(this NdArray<bool> ndarray)
        {
            var len    = ndarray.Length;
            var entity = ndarray.Entity;
            for(var i = 0 ; i < len ; ++i)
                if(entity[i])
                    return true;
            return false;
        }


        /// <summary>
        ///     Determines whether any element of a NdArray satisfies a condition.
        /// </summary>
        /// <param name="ndarray"> An <see cref="NdArray{T}"/> whose elements to apply the predicate to. </param>
        /// <param name="predicate"> A function to test all elements for a condition. </param>
        /// <returns>
        ///     <c>true</c> if any elements in the NdArray pass the test in the specified predicate;
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool Any<T>(this NdArray<T> ndarray, Func<T, bool> predicate)
        {
            var len    = ndarray.Length;
            var entity = ndarray.Entity;
            for(var i = 0 ; i < len ; ++i)
                if(predicate(entity[i]))
                    return true;
            return false;
        }

        #endregion

    }
}
