using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NeodymiumDotNet.Optimizations;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.LinearAlgebra
{
    partial class NdLinAlg
    {
        #region current

        /// <summary>
        ///     <para>Calculates matrix LU decomposition.</para>
        ///     <para>If <paramref name="ndArray"/> cannot decompose to LU, the return values are <c>null</c>.</para>
        ///     <para><c>ndArray == l.dot(u)</c></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static (NdArray<T> l, NdArray<T> u) LU<T>(this INdArray<T> ndArray)
        {
            Guard.AssertShapeMatch(ndArray.Rank != 2 || ndArray.Shape[0] != ndArray.Shape[1], "ndArray must be square matrix.");

            var n = ndArray.Shape[0];
            var a = ndArray.ToMutable();
            for(var k = 0; k < n; ++k)
                if(!ProgressLURecurrence((a.Entity as RawNdArrayImpl<T>)!, k))
                    return default;

            return SplitLU(a);
        }


        /// <summary>
        ///     <para>Calculates matrix LUP decomposition.</para>
        ///     <para><c>ndArray == p.Dot(l).dot(u)</c></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static (NdArray<T> p, NdArray<T> l, NdArray<T> u) LUP<T>(
            this INdArray<T> ndArray,
            IIterationStrategy? strategy = default)
        {
            Guard.AssertShapeMatch(ndArray.Rank == 2 && ndArray.Shape[0] == ndArray.Shape[1], "ndArray must be square matrix.");

            var (l, u, permutations) = ndArray.LUWithPermutations(strategy);
            var n = ndArray.Shape[0];
            return (ToPermutationMatrixFromSet<T>(n, permutations), l, u);
        }


        /// <summary>
        ///     <para>Calculates matrix LU decomposition with permutation set.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static (NdArray<T> l, NdArray<T> u, IReadOnlyList<(int, int)> permutations) LUWithPermutations<T>(
            this INdArray<T> ndArray,
            IIterationStrategy? strategy = default)
        {
            // This function is defined to limit lifetime of stackalloc.
            // Do not expand inline this local function.
            static void exchange(MutableNdArrayImpl<T> a, int argdenom, int k, int j)
                => InternalUtils.Exchange(ref a[stackalloc[] { argdenom, j }],
                                          ref a[stackalloc[] { k, j }]);

            var n = ndArray.Shape[0];
            var a = ndArray.ToMutable();
            var permutations = new List<(int, int)>();
            for(var k = 0; k < n; ++k)
            {
                // pivoting
                var denom = a[k, k];
                var argdenom = k;
                for(var i = k + 1; i < n; ++i)
                {
                    if(NdMath.AbsCompare(a[i, k], denom) > 0)
                    {
                        argdenom = i;
                        denom = a[i, k];
                    }
                }
                if(argdenom > k)
                {
                    permutations.Add((k, argdenom));
                    for(var j = 0; j < n; ++j)
                        exchange(a.Entity, argdenom, k, j);
                }

                // calculating for k-th step
                ProgressLURecurrence((a.Entity as RawNdArrayImpl<T>)!, k, strategy);
            }

            var (l, u) = SplitLU(a);
            return (l, u, permutations);
        }


        /// <summary>
        ///     Updates the step variable for outer-product form gaussian elimination.
        ///     This method modifies <paramref name="a"/> destructively.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <param name="strategy"></param>
        private static bool ProgressLURecurrence<T>(RawNdArrayImpl<T> a, int k, IIterationStrategy? strategy = default)
        {
            var n = a.Shape[0];
            var p = n - k - 1;
            var denom = Divide(One<T>(), a[stackalloc [] { k, k }]);
            if(Equals(Zero<T>(), denom))
                return false;

            void eliminateCellsInColumnK(int row)
                => a[stackalloc[] { row, k }] = Multiply(a[stackalloc[] { row, k }], denom);

            void updateRow(int i, Memory<T> tmpVector)
            {
                VectorOperation.Multiply(a[stackalloc[] { i, k }], a.Buffer.Slice(n * k + k + 1, p), tmpVector);
                VectorOperation.Subtract(a.Buffer.Slice(n * i + k + 1, p), tmpVector, a.Buffer.Slice(n * i + k + 1, p));
            }

            if(strategy is null || strategy is IterationStrategy)
            {
                using var tmpBuffer = new AllocationSlim<T>(p);
                var tmpVector = tmpBuffer.Memory.Slice(0, p);
                for(var i = k + 1; i < n; ++i)
                    eliminateCellsInColumnK(i);
                for(var i = k + 1; i < n; ++i)
                    updateRow(i, tmpVector);
            }
            else
            {
                strategy.For(k + 1, n, eliminateCellsInColumnK);
                strategy.For(k + 1, n, i =>
                {
                    using var tmpBuffer = new AllocationSlim<T>(p);
                    var tmpVector = tmpBuffer.Memory.Slice(0, p);
                    updateRow(i, tmpVector);
                });
            }
            return true;
        }


        /// <summary>
        ///     Splits LU composite matrix.
        ///     This method modifies <paramref name="a"/> destructively.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        private static (NdArray<T> l, NdArray<T> u) SplitLU<T>(MutableNdArray<T> a, IIterationStrategy? strategy = default)
        {
            var n = a.Shape[0];
            var l = NdArray.CreateMutable(new T[n, n]);

            if(strategy is null || strategy is IterationStrategy)
            {
                for(var j = 0; j < n; ++j)
                    for(var i = j + 1; i < n; ++i)
                    {
                        l[i, j] = a[i, j];
                        a[i, j] = Zero<T>();
                    }
                for(var k = 0; k < n; ++k)
                {
                    l[k, k] = One<T>();
                }
            }
            else
            {
                strategy.For(0, n, j =>
                {
                    for(var i = j + 1; i < n; ++i)
                    {
                        l[i, j] = a[i, j];
                        a[i, j] = Zero<T>();
                    }
                });
                strategy.For(0, n, k => l[k, k] = One<T>());
            }
            return (l.MoveToImmutable(), a.MoveToImmutable());
        }

        #endregion


        #region legacy

        /// <summary>
        ///     <para>Calculates matrix LU decomposition.</para>
        ///     <para>If <paramref name="ndArray"/> cannot decompose to LU, the return values are <c>null</c>.</para>
        ///     <para><c>ndArray == l.dot(u)</c></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static (NdArray<T> l, NdArray<T> u) LULegacy<T>(this INdArray<T> ndArray)
        {
            Guard.AssertShapeMatch(ndArray.Rank != 2 || ndArray.Shape[0] != ndArray.Shape[1], "ndArray must be square matrix.");

            var n = ndArray.Shape[0];
            var a = ndArray.ToMutable();
            for(var k = 0; k < n; ++k)
                if(!ProgressLURecurrenceLegacy(a, k))
                    return default;

            return SplitLULegacy(a);
        }


        /// <summary>
        ///     <para>Calculates matrix LUP decomposition.</para>
        ///     <para><c>ndArray == p.Dot(l).dot(u)</c></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        /// <exception cref="ShapeMismatchException"></exception>
        public static (NdArray<T> p, NdArray<T> l, NdArray<T> u) LUPLegacy<T>(
            this INdArray<T> ndArray,
            IIterationStrategy? strategy = default)
        {
            Guard.AssertShapeMatch(ndArray.Rank == 2 && ndArray.Shape[0] == ndArray.Shape[1], "ndArray must be square matrix.");

            var (l, u, permutations) = ndArray.LUWithPermutationsLegacy(strategy);
            var n = ndArray.Shape[0];
            return (ToPermutationMatrixFromSet<T>(n, permutations), l, u);
        }


        /// <summary>
        ///     <para>Calculates matrix LU decomposition with permutation set.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndArray"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static (NdArray<T> l, NdArray<T> u, IReadOnlyList<(int, int)> permutations) LUWithPermutationsLegacy<T>(
            this INdArray<T> ndArray,
            IIterationStrategy? strategy = default)
        {
            // This function is defined to limit lifetime of stackalloc.
            // Do not expand inline this local function.
            static void exchange(MutableNdArrayImpl<T> a, int argdenom, int k, int j)
                => InternalUtils.Exchange(ref a[stackalloc[] { argdenom, j }],
                                          ref a[stackalloc[] { k, j }]);

            var n = ndArray.Shape[0];
            var a = ndArray.ToMutable();
            var permutations = new List<(int, int)>();
            for(var k = 0; k < n; ++k)
            {
                // pivoting
                var denom = a[k, k];
                var argdenom = k;
                for(var i = k + 1; i < n; ++i)
                {
                    if(NdMath.AbsCompare(a[i, k], denom) > 0)
                    {
                        argdenom = i;
                        denom = a[i, k];
                    }
                }
                if(argdenom > k)
                {
                    permutations.Add((k, argdenom));
                    for(var j = 0; j < n; ++j)
                        exchange(a.Entity, argdenom, k, j);
                }

                // calculating for k-th step
                ProgressLURecurrenceLegacy(a, k, strategy);
            }

            var (l, u) = SplitLULegacy(a);
            return (l, u, permutations);
        }


        /// <summary>
        ///     Updates the step variable for outer-product form gaussian elimination.
        ///     This method modifies <paramref name="a"/> destructively.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <param name="strategy"></param>
        private static bool ProgressLURecurrenceLegacy<T>(MutableNdArray<T> a, int k, IIterationStrategy? strategy = default)
        {
            var n = a.Shape[0];
            var denom = Divide(One<T>(), a[k, k]);
            if(Equals(Zero<T>(), denom))
                return false;

            if(strategy is null || strategy is IterationStrategy)
            {
                for(var i = k + 1; i < n; ++i)
                    a[i, k] = Multiply(a[i, k], denom);
                for(var j = k + 1; j < n; ++j)
                {
                    var akj = a[k, j];
                    for(var i = k + 1; i < n; ++i)
                        a[i, j] = Subtract(a[i, j], Multiply(a[i, k], akj));
                }
            }
            else
            {
                strategy.For(k + 1, n, i => a[i, k] = Multiply(a[i, k], denom));
                strategy.For(k + 1, n, j =>
                {
                    var akj = a[k, j];
                    for(var i = k + 1; i < n; ++i)
                        a[i, j] = Subtract(a[i, j], Multiply(a[i, k], akj));
                });
            }
            return true;
        }


        /// <summary>
        ///     Splits LU composite matrix.
        ///     This method modifies <paramref name="a"/> destructively.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        private static (NdArray<T> l, NdArray<T> u) SplitLULegacy<T>(MutableNdArray<T> a, IIterationStrategy? strategy = default)
        {
            var n = a.Shape[0];
            var l = NdArray.CreateMutable(new T[n, n]);

            if(strategy is null || strategy is IterationStrategy)
            {
                for(var j = 0; j < n; ++j)
                    for(var i = j + 1; i < n; ++i)
                    {
                        l[i, j] = a[i, j];
                        a[i, j] = Zero<T>();
                    }
                for(var k = 0; k < n; ++k)
                {
                    l[k, k] = One<T>();
                }
            }
            else
            {
                strategy.For(0, n, j =>
                {
                    for(var i = j + 1; i < n; ++i)
                    {
                        l[i, j] = a[i, j];
                        a[i, j] = Zero<T>();
                    }
                });
                strategy.For(0, n, k => l[k, k] = One<T>());
            }
            return (l.MoveToImmutable(), a.MoveToImmutable());
        }

        #endregion
    }
}
