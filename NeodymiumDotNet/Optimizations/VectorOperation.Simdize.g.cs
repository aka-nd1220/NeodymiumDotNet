using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;


namespace NeodymiumDotNet.Optimizations
{
    partial class VectorOperation
    {

        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<Memory<T>> Simdize<T>(Expression<Func<T>> expr)
            where T : unmanaged
            => Simdize<T, Action<Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);


        /// <summary>
        ///     Converts the specified operation for primitive type to the operation delegate for primitive array with SIMD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>> Simdize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expr)
            where T : unmanaged
            => Simdize<T, Action<ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, ReadOnlyMemory<T>, Memory<T>>>(expr);

    }
}
