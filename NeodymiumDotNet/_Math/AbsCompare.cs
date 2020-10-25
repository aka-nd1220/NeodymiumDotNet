using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using NeodymiumDotNet.Specialization;

namespace NeodymiumDotNet
{
    partial class NdMath
    {
        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(sbyte x, sbyte y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(short x, short y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(int x, int y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(long x, long y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(float x, float y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(double x, double y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(decimal x, decimal y)
            => Abs(x).CompareTo(Abs(y));


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare(Complex x, Complex y)
            => x.Magnitude.CompareTo(y.Magnitude);


        /// <summary>
        ///     Compares absolute values of two specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relationship between the two comparands.
        ///     <list type="table">
        ///         <item>
        ///             <term> Less than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is smaller than <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is same with <paramref name="y"/>'s one. </description>
        ///         </item>
        ///         <item>
        ///             <term> Greater than 0 </term>
        ///             <description> The absolute of <paramref name="x"/> is larger than <paramref name="y"/>'s one. . </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AbsCompare<T>(T x, T y)
        {
            if(typeof(T) == typeof(sbyte  )) return AbsCompare(x.As<T, sbyte  >(), y.As<T, sbyte  >());
            if(typeof(T) == typeof(short  )) return AbsCompare(x.As<T, short  >(), y.As<T, short  >());
            if(typeof(T) == typeof(int    )) return AbsCompare(x.As<T, int    >(), y.As<T, int    >());
            if(typeof(T) == typeof(long   )) return AbsCompare(x.As<T, long   >(), y.As<T, long   >());
            if(typeof(T) == typeof(float  )) return AbsCompare(x.As<T, float  >(), y.As<T, float  >());
            if(typeof(T) == typeof(double )) return AbsCompare(x.As<T, double >(), y.As<T, double >());
            if(typeof(T) == typeof(decimal)) return AbsCompare(x.As<T, decimal>(), y.As<T, decimal>());
            if(typeof(T) == typeof(Complex)) return AbsCompare(x.As<T, Complex>(), y.As<T, Complex>());

            var xabs = DoubleAbs(x);
            var yabs = DoubleAbs(y);
            return xabs.CompareTo(yabs);
        }
    }
}
