using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeodymiumDotNet.Random
{
    /// <summary>
    ///     Random operation for <see cref="NdArray{T}"/>.
    /// </summary>
    public static class RandomNdArray
    {
        /// <summary>
        ///     Gets a <see cref="NdArray{T}"/> whose values are <c>int.MinValue - int.MaxValue</c> random <see cref="int"/> value.
        /// </summary>
        /// <param name="shape"> [Non-Null] </param>
        /// <param name="gen"></param>
        /// <returns></returns>
        public static NdArray<int> RandInt32(int[] shape, RandomGenerator? gen = default)
        {
            Guard.AssertArgumentNotNull(shape, nameof(shape));
            if(gen == null)
                gen = RandomGenerator.Default;

            return NdArray
               .Create(gen.NextInt32(shape.Aggregate((x, y) => x * y)), shape);
        }

        /// <summary>
        ///     Gets a <see cref="NdArray{T}"/> whose values are <c>long.MinValue - long.MaxValue</c> random <see cref="long"/> value.
        /// </summary>
        /// <param name="shape"> [Non-Null] </param>
        /// <param name="gen"></param>
        /// <returns></returns>
        public static NdArray<long> RandInt64(int[] shape, RandomGenerator? gen = default)
        {
            Guard.AssertArgumentNotNull(shape, nameof(shape));
            if(gen == null)
                gen = RandomGenerator.Default;

            return NdArray
               .Create(gen.NextInt64(shape.Aggregate((x, y) => x * y)), shape);
        }

        /// <summary>
        ///     Gets a <see cref="NdArray{T}"/> whose values are <c>0 - 1</c> random <see cref="float"/> value.
        /// </summary>
        /// <param name="shape"> [Non-Null] </param>
        /// <param name="gen"></param>
        /// <returns></returns>
        public static NdArray<float> Rand32(int[] shape, RandomGenerator? gen = default)
        {
            Guard.AssertArgumentNotNull(shape, nameof(shape));
            if(gen == null)
                gen = RandomGenerator.Default;

            return NdArray
               .Create(gen.NextFloat32(shape.Aggregate((x, y) => x * y)), shape);
        }


        /// <summary>
        ///     Gets a <see cref="NdArray{T}"/> whose values are normal distribution random <see cref="float"/> value.
        /// </summary>
        /// <param name="shape"> [Non-Null] </param>
        /// <param name="gen"></param>
        /// <returns></returns>
        public static NdArray<float> RandN32(int[] shape, RandomGenerator? gen = default)
        {
            Guard.AssertArgumentNotNull(shape, nameof(shape));
            if(gen == null)
                gen = RandomGenerator.Default;

            return NdArray
               .Create(gen.NextNorm32(shape.Aggregate((x, y) => x * y)), shape);
        }


        /// <summary>
        ///     Gets a <see cref="NdArray{T}"/> whose values are <c>0 - 1</c> random <see cref="double"/> value.
        /// </summary>
        /// <param name="shape"> [Non-Null] </param>
        /// <param name="gen"></param>
        /// <returns></returns>
        public static NdArray<double> Rand64(int[] shape, RandomGenerator? gen = default)
        {
            Guard.AssertArgumentNotNull(shape, nameof(shape));
            if(gen is null)
                gen = RandomGenerator.Default;

            return NdArray
               .Create(gen.NextFloat64(shape.Aggregate((x, y) => x * y)), shape);
        }


        /// <summary>
        ///     Gets a <see cref="NdArray{T}"/> whose values are normal distribution random <see cref="double"/> value.
        /// </summary>
        /// <param name="shape"> [Non-Null] </param>
        /// <param name="gen"></param>
        /// <returns></returns>
        public static NdArray<double> RandN64(int[] shape, RandomGenerator? gen = default)
        {
            Guard.AssertArgumentNotNull(shape, nameof(shape));
            if(gen == null)
                gen = RandomGenerator.Default;

            return NdArray
               .Create(gen.NextNorm64(shape.Aggregate((x, y) => x * y)), shape);
        }

    }
}
