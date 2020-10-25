using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NeodymiumDotNet.Optimizations
{
    internal static class MemberTable
    {
        private const BindingFlags _publicInstance = BindingFlags.Public | BindingFlags.Instance;
        private const BindingFlags _publicStatic = BindingFlags.Public | BindingFlags.Static;


        public static class _Console
        {
            public static MethodInfo? WriteLineObject { get; }
                = typeof(Console)
                  .GetMethod(nameof(Console.WriteLine), _publicStatic, typeof(object));
        }


        public static class _Math
        {
            public static class Abs<T>
            {
                public static MethodInfo? Method { get; }
                    = typeof(Math).GetMethod(nameof(Math.Abs), _publicStatic, typeof(T));
            }

            public static class Max<T>
            {
                public static MethodInfo? Method { get; }
                    = typeof(Math).GetMethod(nameof(Math.Max), _publicStatic, typeof(T), typeof(T));
            }

            public static class Min<T>
            {
                public static MethodInfo? Method { get; }
                    = typeof(Math).GetMethod(nameof(Math.Min), _publicStatic, typeof(T), typeof(T));
            }

            public static class Sqrt<T>
            {
                public static MethodInfo? Method { get; }
                    = typeof(Math).GetMethod(nameof(Math.Sqrt), _publicStatic, typeof(T));
            }
        }


        public static class _Vector
        {
            public static class Max<T> where T : unmanaged
            {
                public static MethodInfo? Method { get; }
                    = typeof(Vector).GetMethod(nameof(Vector.Max), _publicStatic, new[] {typeof(T)}, new[] {typeof(Vector<T>), typeof(Vector<T>)});
            }

            public static class Min<T> where T : unmanaged
            {
                public static MethodInfo? Method { get; }
                    = typeof(Vector).GetMethod(nameof(Vector.Min), _publicStatic, new[] { typeof(T) }, new[] { typeof(Vector<T>), typeof(Vector<T>) });
            }

            public static class Sqrt<T> where T : unmanaged
            {
                public static MethodInfo? Method { get; }
                    = typeof(Vector).GetMethod(nameof(Vector.SquareRoot), _publicStatic, new[] { typeof(T) }, new[] { typeof(Vector<T>) });
            }
        }


        public static class _Vector<T>
            where T : unmanaged
        {
            public static PropertyInfo Count { get; }
                = typeof(Vector<T>)
                  .GetProperty(nameof(Vector<T>.Count), _publicStatic);

            public static PropertyInfo Item { get; }
                = typeof(Vector<T>).GetProperties(_publicInstance)
                  .First(p => p.GetIndexParameters()
                               .Select(para => para.ParameterType)
                               .SequenceEqual(new[] { typeof(int) })
                         );

            public static ConstructorInfo ScaleConstructor { get; }
                = typeof(Vector<T>)
                  .GetConstructor(new[] { typeof(T) });

            public static ConstructorInfo ArrayConstructor { get; }
                = typeof(Vector<T>)
                  .GetConstructor(new[] { typeof(T[]) });
        }


        public static class _Memory<T>
            where T : unmanaged
        {
            public static PropertyInfo Span { get; }
                = typeof(Memory<T>)
                  .GetProperty(nameof(Memory<T>.Span), _publicInstance);
        }


        public static class _ReadOnlyMemory<T>
            where T : unmanaged
        {
            public static PropertyInfo Span { get; }
                = typeof(ReadOnlyMemory<T>)
                  .GetProperty(nameof(ReadOnlyMemory<T>.Span), _publicInstance);
        }


        public static class _Span<T>
            where T : unmanaged
        {
            public static PropertyInfo Length { get; }
                = typeof(Span<T>)
                  .GetProperty(nameof(Span<T>.Length), _publicInstance);

            public static MethodInfo GetItem { get; }
                = typeof(_Span<T>)
                  .GetMethod(nameof(GetItemCore), _publicStatic);

            public static MethodInfo SetItem { get; }
                = typeof(_Span<T>)
                  .GetMethod(nameof(SetItemCore), _publicStatic);

            public static T GetItemCore(Span<T> span, int index) => span[index];
            public static T SetItemCore(Span<T> span, int index, T value) => span[index] = value;
        }


        public static class _ReadOnlySpan<T>
            where T : unmanaged
        {
            public static PropertyInfo Length { get; }
                = typeof(ReadOnlySpan<T>)
                  .GetProperty(nameof(ReadOnlySpan<T>.Length), _publicInstance);

            public static MethodInfo GetItem { get; }
                = typeof(_ReadOnlySpan<T>)
                  .GetMethod(nameof(GetItemCore), _publicStatic);

            public static T GetItemCore(ReadOnlySpan<T> span, int index) => span[index];
        }


        public static class _MemoryMarshal
        {
            public static class Cast<TFrom, TTo>
            {
                public static MethodInfo ForSpan { get; }
                    = typeof(MemoryMarshal)
                        .GetMethod(nameof(MemoryMarshal.Cast), _publicStatic, new[] { typeof(TFrom), typeof(TTo) }, new[] { typeof(Span<TFrom>) })
                        ?? throw new NotSupportedException();

                public static MethodInfo ForReadOnlySpan { get; }
                    = typeof(MemoryMarshal)
                        .GetMethod(nameof(MemoryMarshal.Cast), _publicStatic, new[] { typeof(TFrom), typeof(TTo) }, new[] { typeof(ReadOnlySpan<TFrom>) })
                        ?? throw new NotSupportedException();
            }
        }
    }
}
