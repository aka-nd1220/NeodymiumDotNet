using System;
using System.Collections.Generic;
using System.Text;
using NeodymiumDotNet.Optimizations;

namespace NeodymiumDotNet.Experiment
{
    partial class Lapack
    {
        // reference:
        //   https://github.com/xianyi/OpenBLAS/blob/develop/reference/dlaswpf.f
        /// <summary>
        ///     [dlaswp] Swaps rows on the matrix A.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="colstart"></param>
        /// <param name="collength"></param>
        /// <param name="ipiv"></param>
        /// <remarks> The columns between <c>[colstart, colstart + collength)</c> </remarks>
        public static void SwapRows<T>(MutableNdArray<T> a, int colstart, int collength, ReadOnlySpan<int> ipiv)
        {
            Guard.AssertArgumentRange(0 <= colstart, "0 <= colstart");
            Guard.AssertArgumentRange(colstart + collength < a.Shape[1], "colstart + collength < a.Shape[1]");

            if(a is RawNdArray<T> xa)
            {
                using var alloc = new AllocationSlim<T>(collength);
                var tmp = alloc.Memory.Slice(0, collength);
                var c = a.Shape[1];
                for(var i = 0; i < ipiv.Length; ++i)
                {
                    if(i == ipiv[i])
                        continue;
                    var row1 = xa.Entity.Buffer.Slice(c * ipiv[i] + colstart, collength);
                    var row2 = xa.Entity.Buffer.Slice(c * i       + colstart, collength);
                    VectorOperation.Identity(row1, tmp);
                    VectorOperation.Identity(row2, row1);
                    VectorOperation.Identity(tmp, row2);
                }
            }
            else
            {
                for(var i = 0; i < ipiv.Length; ++i)
                {
                    if(i == ipiv[i])
                        continue;
                    for(var j = colstart; j < colstart + collength; ++j)
                    {
                        var tmp = a[i, j];
                        a[i, j] = a[ipiv[i], j];
                        a[ipiv[i], j] = tmp;
                    }
                }
            }
        }
    }
}
