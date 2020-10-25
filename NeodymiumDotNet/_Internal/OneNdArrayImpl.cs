using System;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    internal sealed class OneNdArrayImpl<T> : NdArrayImpl<T>, IBufferNdArrayImpl<T>
    {
        private static readonly object _arrayCacheToken = new object();

        private static T[] _globalBufferCache = Array.Empty<T>();

        ReadOnlyMemory<T> IBufferNdArrayImpl<T>.Buffer
        {
            get
            {
                var array = _globalBufferCache;
                if(array.Length > Length)
                    return array.AsMemory(0, Length);
                lock(_arrayCacheToken)
                {
                    array = new T[InternalUtils.CeilingPow2(Length)];
                    for(var i = 0; i < array.Length; ++i)
                        array[i] = ValueTrait.One<T>();
                    _globalBufferCache = array;
                }
                return array.AsMemory(0, Length);
            }
        }


        internal OneNdArrayImpl(IndexArray shape) : base(shape)
            => Guard.AssertCast(ValueTrait.IsNumber<T>(),
                                        $"{typeof(T)} has no available one value.");


        protected override T GetItem(int flattenIndex) => ValueTrait.One<T>();

        protected override T GetItem(ReadOnlySpan<int> shapedIndices) => ValueTrait.One<T>();

        protected override void CopyToCore(Span<T> dest)
        {
            for(var i = 0; i < Length; ++i)
                dest[i] = ValueTrait.One<T>();
        }
    }
}
