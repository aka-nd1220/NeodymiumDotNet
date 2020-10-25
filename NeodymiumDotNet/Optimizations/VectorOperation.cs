using System;

namespace NeodymiumDotNet.Optimizations
{
    /// <summary>
    ///     Provides operation conversions from SISD to SIMD.
    /// </summary>
    public static partial class VectorOperation
    {
        /// <summary>
        ///     Applies identity operations for a n-d array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"> It can be same instance with <paramref name="value"/>, and in that case the instance will be overwritten. </param>
        /// <exception cref="ShapeMismatchException" />
        /// <exception cref="NotSupportedException" />
        public static void Identity<T>(INdArray<T> value, MutableNdArray<T> result)
        {
            Guard.AssertShapeMatch(value.Shape == result.Shape, "There is shape mismatch.");
            if(value.TryGetBufferImpl(out var xvalue) && result is RawNdArray<T> xresult)
            {
                Identity(xvalue.Buffer, xresult.Entity.Buffer);
            }
            else
            {
                for(var i = 0; i < result.Length; ++i)
                    result.SetItem(i, value.GetItem(i));
            }
        }

        /// <summary>
        ///     Applies identity operations for a memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        public static void Identity<T>(ReadOnlyMemory<T> value, Memory<T> result)
        {
            Guard.AssertArgumentRange(result.Length <= value.Length, "result.Length <= value.Length");
            value.Slice(0, result.Length).CopyTo(result);
        }
    }
}
