using System;
using System.Runtime.CompilerServices;

namespace NeodymiumDotNet.Specialization
{
    internal static class SpecializationUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TTo As<TFrom, TTo>(this TFrom from)
            => from is TTo to ? to : throw new NeverException();

        
        public class NeverException : Exception
        {
            public override string Message
                => "Logically this branch will never be pass. If it's thrown, please issue the library developpers.";
        }
    }
}
