using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NeodymiumDotNet
{
    internal static class NdArrayInternal
    {
        public static bool TryGetBufferImpl<T>(this INdArray<T> array, [NotNullWhen(true)] out IBufferNdArrayImpl<T>? result)
        {
            if((array as INdArrayInternal<T>)?.Entity is IBufferNdArrayImpl<T> xresult)
            {
                result = xresult;
                return true;
            }
            result = default;
            return false;
        }
    }


    internal interface INdArrayInternal<T> : INdArray<T>
    {
        NdArrayImpl<T> Entity { get; }
    }


    internal interface IBufferNdArrayImpl<T> : INdArrayImpl<T>
    {
        ReadOnlyMemory<T> Buffer { get; }
    }
}
