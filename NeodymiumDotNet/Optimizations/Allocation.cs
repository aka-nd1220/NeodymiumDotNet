using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace NeodymiumDotNet.Optimizations
{
    /// <summary>
    /// Resolves array pool repaying by disposer or finalizer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Allocation<T> : IDisposable
    {
        private readonly int _minLength;

        /// <summary>
        /// Gets the reserved array.
        /// </summary>
        public T[] Array => _array ?? throw new ObjectDisposedException(ToString());

        private T[]? _array;

        /// <summary>
        /// Gets the reserved array as <see cref="Memory{T}"/> which is sized as just the specified length.
        /// </summary>
        public Memory<T> Memory => Array.AsMemory(0, _minLength);

        /// <summary>
        /// Gets the reserved array as <see cref="Span{T}"/> which is sized as just the specified length.
        /// </summary>
        public Span<T> Span => Array.AsSpan(0, _minLength);

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="minLength"></param>
        public Allocation(int minLength)
        {
            _minLength = minLength;
            _array = ArrayPool<T>.Shared.Rent(minLength);
        }

        /// <summary>
        /// Frees allocated array.
        /// </summary>
        public void Dispose()
        {
            var array = Interlocked.Exchange(ref _array, null);
            ArrayPool<T>.Shared.Return(array);
        }

        /// <summary>
        /// Frees allocated array if it has not been released yet.
        /// </summary>
        ~Allocation()
        {
            if(_array is null)
                return;
            Dispose();
        }
    }


    /// <summary>
    /// Resolves array pool repaying by ref disposer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public ref struct AllocationSlim<T>
    {
        private readonly int _minLength;

        /// <summary>
        /// Gets the reserved array.
        /// </summary>
        public T[] Array { get; }

        /// <summary>
        /// Gets the reserved array as <see cref="Memory{T}"/> which is sized as just the specified length.
        /// </summary>
        public Memory<T> Memory => Array.AsMemory(0, _minLength);

        /// <summary>
        /// Gets the reserved array as <see cref="Span{T}"/> which is sized as just the specified length.
        /// </summary>
        public Span<T> Span => Array.AsSpan(0, _minLength);

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="minLength"></param>
        public AllocationSlim(int minLength)
        {
            _minLength = minLength;
            Array = ArrayPool<T>.Shared.Rent(minLength);
        }

        /// <summary>
        /// Frees allocated array.
        /// </summary>
        public void Dispose()
        {
            if(Array is null)
                return;
            ArrayPool<T>.Shared.Return(Array);
        }
    }
}
