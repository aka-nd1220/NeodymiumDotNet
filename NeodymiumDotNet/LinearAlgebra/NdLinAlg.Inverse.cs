using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        partial class Cache<T>
        {
            public static IReadOnlyList<T> Empty { get; } = new List<T>();
        }


        /// <summary>
        ///     Calculates inverse matrix.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        public static NdArray<T> Inverse<T>(this INdArray<T> ndArray)
        {
            Guard.AssertShapeMatch((bool)(ndArray.Rank == 2 & ndArray.Shape[0] == ndArray.Shape[1]), "ndArray.Rank == 2 & ndArray.Shape[0] == ndArray.Shape[1]");

            var b = Identity<T>((int)ndArray.Shape[0]);
            var (l, u, perms) = NdLinAlg.LUWithPermutationsLegacy<T>(ndArray);
            var xx = NdArray.CreateMutable(new T[b.Shape[0], b.Shape[1]]);
            var x = xx.Entity;
            var row = b.Shape[0];
            var col = b.Shape[1];
            for(var j = 0; j < col; ++j)
            {
                var bj = b[Range.Whole, new Index(j, false)];
                var xj = xx[Range.Whole, new Index(j, false)];
                SolveCore(l, u, perms, bj, xj);
            }

            for(var k = perms.Count - 1; k >= 0; --k)
            {
                var (p, q) = perms[k];
                for(var i = 0 ; i < row ; ++i)
                {
                    Span<int> idx1 = stackalloc int[] { i, p };
                    Span<int> idx2 = stackalloc int[] { i, q };
                    InternalUtils.Exchange(ref x[idx1], ref x[idx2]);
                }
            }
            for(var k = perms.Count - 1; k >= 0; --k)
            {
                var (p, q) = perms[k];
                for(var i = 0; i < row; ++i)
                {
                    Span<int> idx1 = stackalloc int[] { i, p };
                    Span<int> idx2 = stackalloc int[] { i, q };
                    InternalUtils.Exchange(ref x[idx1], ref x[idx2]);
                }
            }
            return xx.MoveToImmutable();
        }
    }
}
