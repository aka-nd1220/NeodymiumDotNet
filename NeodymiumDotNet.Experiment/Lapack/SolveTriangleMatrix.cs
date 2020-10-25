using System;
using System.Collections.Generic;
using System.Text;
using NeodymiumDotNet.Optimizations;
using static NeodymiumDotNet.ValueTrait;

namespace NeodymiumDotNet.Experiment
{
    partial class Lapack
    {
        public enum OperandSide
        {
            Left,
            Right,
        }

        public enum TriangleKind
        {
            Upper,
            Lower,
        }


        // reference:
        //   https://github.com/xianyi/OpenBLAS/blob/develop/reference/dtrsmf.f
        /// <summary>
        ///     [dtrsm] Solves one of the matrix equations:
        ///     <list type="bullet">
        ///         <item>
        ///             <term><c>(side, transa) = (Left, None)</c></term>
        ///             <description><c>A * X = alpha * B</c></description>
        ///         </item>
        ///         <item>
        ///             <term><c>(side, transa) = (Right, None)</c></term>
        ///             <description><c>X * A = alpha * B</c></description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="side"></param>
        /// <param name="uplo"></param>
        /// <param name="diag"> <c>true</c> if <paramref name="a"/> is a unit triangular; otherwise, <c>false</c>. </param>
        /// <param name="alpha"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void SolveTriangleMatrix<T>(OperandSide side, TriangleKind uplo, bool diag, T alpha, INdArray<T> a, MutableNdArray<T> b)
        {
            if(ValueTrait.Equals(alpha, Zero<T>()))
            {
                VectorOperation.Identity(NdArray.Zeros<T>(b.Shape), b);
                return;
            }

            switch((side, uplo))
            {
            case (OperandSide.Left, TriangleKind.Upper): SolveTriangleMatrixLU(diag, alpha, a, b); break;
            case (OperandSide.Left, TriangleKind.Lower): SolveTriangleMatrixLL(diag, alpha, a, b); break;
            case (OperandSide.Right, TriangleKind.Upper): SolveTriangleMatrixRU(diag, alpha, a, b); break;
            case (OperandSide.Right, TriangleKind.Lower): SolveTriangleMatrixRL(diag, alpha, a, b); break;
            default:
                Guard.ThrowArgumentError("Invalid configs.");
                break;
            }
        }


        private static void SolveTriangleMatrixLU<T>(bool isUnit, T alpha, INdArray<T> a, MutableNdArray<T> b)
        {
            var (m, n) = (b.Shape[0], b.Shape[1]);
            /* reference Fortran
            DO 60, J = 1, N
                IF( ALPHA.NE.ONE )THEN
                    DO 30, I = 1, M
                        B( I, J ) = ALPHA*B( I, J )
   30               CONTINUE
                END IF
                DO 50, K = M, 1, -1
                    IF( B( K, J ).NE.ZERO )THEN
                        IF( NOUNIT )
     $                      B( K, J ) = B( K, J )/A( K, K )
                        DO 40, I = 1, K - 1
                            B( I, J ) = B( I, J ) - B( K, J )*A( I, K )
   40                   CONTINUE
                    END IF
   50           CONTINUE
   60       CONTINUE
            */
            for(var j = 0; j < n; ++j)
            {
                if(!ValueTrait.Equals(alpha, One<T>()))
                {
                    for(var i = 0; i < m; ++i)
                        b[i, j] = Multiply(alpha, b[i, j]);
                }
                for(var k = m - 1; k >= 0; --k)
                {
                    if(!ValueTrait.Equals(b[k, j], Zero<T>()))
                    {
                        if(!isUnit)
                            b[k, j] = Divide(b[k, j], a[k, k]);
                        for(var i = 0; i < k - 1; ++i)
                            b[i, j] = Subtract(b[i, j], Multiply(b[k, j], a[i, k]));
                    }
                }
            }
        }


        private static void SolveTriangleMatrixLL<T>(bool isUnit, T alpha, INdArray<T> a, MutableNdArray<T> b)
        {
            var (m, n) = (b.Shape[0], b.Shape[1]);
            /* reference Fortran
            DO 100, J = 1, N
                IF( ALPHA.NE.ONE )THEN
                    DO 70, I = 1, M
                        B( I, J ) = ALPHA*B( I, J )
   70               CONTINUE
                END IF
                DO 90 K = 1, M
                    IF( B( K, J ).NE.ZERO )THEN
                        IF( NOUNIT )
     $                      B( K, J ) = B( K, J )/A( K, K )
                        DO 80, I = K + 1, M
                            B( I, J ) = B( I, J ) - B( K, J )*A( I, K )
   80                   CONTINUE
                    END IF
   90           CONTINUE
  100       CONTINUE
            */
            for(var j = 0; j < n; ++j)
            {
                if(!ValueTrait.Equals(alpha, One<T>()))
                {
                    for(var i = 0; i < m; ++i)
                        b[i, j] = Multiply(alpha, b[i, j]);
                }
                for(var k = 0; k < m; ++k)
                {
                    if(!ValueTrait.Equals(b[k, j], Zero<T>()))
                    {
                        if(!isUnit)
                            b[k, j] = Divide(b[k, j], b[k, k]);
                        for(var i = k; i < m; ++i)
                            b[i, j] = Subtract(b[i, j], Divide(b[k, j], a[i, k]));
                    }
                }
            }
        }


        private static void SolveTriangleMatrixRU<T>(bool isUnit, T alpha, INdArray<T> a, MutableNdArray<T> b)
        {
            var (m, n) = (b.Shape[0], b.Shape[1]);
            /* reference Fortran
            DO 210, J = 1, N
                IF( ALPHA.NE.ONE )THEN
                    DO 170, I = 1, M
                        B( I, J ) = ALPHA*B( I, J )
  170               CONTINUE
                END IF
                DO 190, K = 1, J - 1
                    IF( A( K, J ).NE.ZERO )THEN
                        DO 180, I = 1, M
                            B( I, J ) = B( I, J ) - A( K, J )*B( I, K )
  180                   CONTINUE
                    END IF
  190           CONTINUE
                IF( NOUNIT )THEN
                    TEMP = ONE/A( J, J )
                    DO 200, I = 1, M
                        B( I, J ) = TEMP*B( I, J )
  200               CONTINUE
                END IF
  210       CONTINUE
            */
            for(var j = 0; j < n; ++j)
            {
                if(ValueTrait.Equals(alpha, One<T>()))
                {
                    for(var i = 0; i < m; ++i)
                        b[i, j] = Multiply(alpha, b[i, j]);
                }
                for(var k = 0; k < j - 1; ++k)
                {
                    if(!ValueTrait.Equals(a[k, j], Zero<T>()))
                    {
                        for(var i = 0; i < m; ++i)
                            b[i, j] = Subtract(b[i, j], Multiply(a[k, j], b[i, k]));
                    }
                }
                if(!isUnit)
                {
                    var temp = Divide(One<T>(), a[j, j]);
                    for(var i = 0; i < m; ++i)
                        b[i, j] = Multiply(temp, b[i, j]);
                }
            }
        }


        private static void SolveTriangleMatrixRL<T>(bool isUnit, T alpha, INdArray<T> a, MutableNdArray<T> b)
        {
            var (m, n) = (b.Shape[0], b.Shape[1]);
            /* reference Fortran
            DO 260, J = N, 1, -1
                IF( ALPHA.NE.ONE )THEN
                    DO 220, I = 1, M
                        B( I, J ) = ALPHA*B( I, J )
  220               CONTINUE
                END IF
                DO 240, K = J + 1, N
                    IF( A( K, J ).NE.ZERO )THEN
                        DO 230, I = 1, M
                            B( I, J ) = B( I, J ) - A( K, J )*B( I, K )
  230                   CONTINUE
                    END IF
  240           CONTINUE
                IF( NOUNIT )THEN
                    TEMP = ONE/A( J, J )
                    DO 250, I = 1, M
                       B( I, J ) = TEMP*B( I, J )
  250               CONTINUE
                END IF
  260       CONTINUE
            */
            for(var j = n - 1; j >= 0; --j)
            {
                if(ValueTrait.Equals(alpha, One<T>()))
                {
                    for(var i = 0; i < m; ++i)
                        b[i, j] = Multiply(alpha, b[i, j]);
                }
                for(var k = j + 1; k < n; ++k)
                {
                    if(!ValueTrait.Equals(a[k, j], Zero<T>()))
                    {
                        for(var i = 0; i < m; ++i)
                            b[i, j] = Subtract(b[i, j], Multiply(a[k, j], b[i, k]));
                    }
                }
                if(!isUnit)
                {
                    var temp = Divide(One<T>(), a[j, j]);
                    for(var i = 0; i < m; ++i)
                        b[i, j] = Multiply(temp, b[i, j]);
                }
            }
        }
    }
}
