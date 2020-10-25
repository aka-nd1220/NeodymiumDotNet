using System;
using System.Collections.Generic;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        /// <summary>
        ///     Calculates a matrix determinant.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static T Determinant<T>(this INdArray<T> ndArray, IIterationStrategy? strategy = default)
        {
            Guard.AssertShapeMatch(ndArray.Shape.Length == 2 && ndArray.Shape[0] == ndArray.Shape[1],
                                         "x must be a square matrix.");

            var (_, u, permutations) = ndArray.LUWithPermutationsLegacy(strategy);
            var n = ndArray.Shape[0];
            var x = One<T>();
            for(var i = 0; i < n; ++i)
                x = Multiply(x, u[i, i]);
            if(permutations.Count % 2 == 1)
                x = UnaryNegate(x);
            return x;
        }
    }
}
