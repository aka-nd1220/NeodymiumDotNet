using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     Gets the permutation matrix from permutation set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="n"></param>
        /// <param name="permutations"></param>
        /// <returns></returns>
        public static NdArray<T> ToPermutationMatrixFromSet<T>(int n, IReadOnlyList<(int, int)> permutations)
        {
            // This function is defined to limit lifetime of stackalloc.
            // Do not expand inline this local function.
            static void exchange(MutableNdArrayImpl<T> p, int j, (int, int) perm)
                => InternalUtils.Exchange(ref p[stackalloc[] { j, perm.Item1 }],
                                          ref p[stackalloc[] { j, perm.Item2 }]);

            var pp = Identity<T>(n).ToMutable();
            var p = pp.Entity;
            foreach(var perm in permutations)
                for(var j = 0; j < n; ++j)
                    exchange(p, j, perm);
            return pp.ToImmutable();
        }


        // TODO: implement
        /*
        public static IReadOnlyList<(int, int)> ToPermutationSetFromMatrix<T>(NdArray<T> permutationMatrix)
        {
        }
        */
    }
}
