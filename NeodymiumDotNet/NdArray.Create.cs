using System;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Provides operators for <see cref="NdArray{T}"/>.
    /// </summary>
    public static partial class NdArray
    {

        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has any shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="shape"> [<c>array.Length == shape.TotalLength</c>] </param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[] array, IndexArray shape)
        {
            var entity = new RawNdArrayImpl<T>(shape);
            array.CopyTo(entity.Buffer.Span);
            return new NdArray<T>(entity);
        }


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 1-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 2-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[,] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 3-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[,,] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 4-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[,,,] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 5-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[,,,,] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 6-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[,,,,,] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates immutable <see cref="NdArray{T}"/> instance which has 7-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static NdArray<T> Create<T>(T[,,,,,,,] array)
            => new NdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has any shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"> [<c>array.Length == shape.TotalLength</c>] </param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(IndexArray shape)
        {
            var entity = new RawNdArrayImpl<T>(shape);
            return new MutableNdArray<T>(entity);
        }


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has any shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="shape"> [<c>array.Length == shape.TotalLength</c>] </param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[] array, IndexArray shape)
        {
            var entity = new RawNdArrayImpl<T>(shape);
            array.CopyTo(entity.Buffer.Span);
            return new MutableNdArray<T>(entity);
        }


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 1-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 2-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[,] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 3-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[,,] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 4-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[,,,] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 5-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[,,,,] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 6-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[,,,,,] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates mutable <see cref="NdArray{T}"/> instance which has 7-rank shape.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static MutableNdArray<T> CreateMutable<T>(T[,,,,,,,] array)
            => new MutableNdArray<T>(array);


        /// <summary>
        ///     Creates a <see cref="NdArray{T}"/> instance whose all elements are zero.
        /// </summary>
        /// <typeparam name="T"> [Primitive] </typeparam>
        /// <param name="shape"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"> <typeparamref name="T"/> is not primitive type. </exception>
        public static NdArray<T> Zeros<T>(IndexArray shape)
            => new NdArray<T>(new ZeroNdArrayImpl<T>(shape));


        /// <summary>
        ///     Creates a <see cref="NdArray{T}"/> instance whose all elements are zero.
        /// </summary>
        /// <typeparam name="T"> [Primitive] </typeparam>
        /// <param name="shape"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"> <typeparamref name="T"/> is not primitive type. </exception>
        public static NdArray<T> Zeros<T>(ReadOnlySpan<int> shape)
            => new NdArray<T>(new IndexArray(shape));


        /// <summary>
        ///     Creates a <see cref="NdArray{T}"/> instance whose all elements are zero.
        /// </summary>
        /// <typeparam name="T"> [Primitive] </typeparam>
        /// <param name="shape"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"> <typeparamref name="T"/> is not primitive type. </exception>
        public static NdArray<T> Zeros<T>(int[] shape)
            => new NdArray<T>(new IndexArray(shape));


        /// <summary>
        ///     Creates a <see cref="NdArray{T}"/> instance whose all elements are one.
        /// </summary>
        /// <typeparam name="T"> [Primitive] </typeparam>
        /// <param name="shape"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"> <typeparamref name="T"/> is not primitive type. </exception>
        public static NdArray<T> Ones<T>(int[] shape)
            => new NdArray<T>(new OneNdArrayImpl<T>(new IndexArray(shape)));

    }
}
