using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     Gets the identity matrix which has the specified dimension.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="n"></param>
        /// <returns></returns>
        public static NdArray<T> Identity<T>(int n)
        {
            var array = NdArray.CreateMutable(new T[n, n]);
            for(var i = 0; i < n; ++i)
                array[i, i] = ValueTrait.One<T>();

            return array.MoveToImmutable();
        }
    }
}
