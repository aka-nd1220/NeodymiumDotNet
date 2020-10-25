using System;
using System.Collections.Generic;
using System.Linq;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Additional Linq to Object operators.
    /// </summary>
    internal static class EnumerableEx
    {

        /// <summary>
        ///     Returns the item if <paramref name="source"/> has only an item;
        ///     otherwise <c>default</c>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static TSource SingleWhenOnly<TSource>(this IEnumerable<TSource> source)
        {
            Guard.AssertArgumentNotNull(source, nameof(source));
            var retval = default(TSource)!;
            var count = 0;
            foreach(var item in source.Take(2))
            {
                retval = item;
                ++count;
            }

#nullable disable
            return count == 1 ? retval : default;
#nullable enable
        }


        /// <summary>
        ///     Zips 2 sequences to tuple sequence.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <returns></returns>
        internal static IEnumerable<(T1, T2)> Zip<T1, T2>(
            this IEnumerable<T1> src1, IEnumerable<T2> src2)
        {
            IEnumerable<(T1, T2)> core()
            {
                var en1 = src1.GetEnumerator();
                var en2 = src2.GetEnumerator();
                for(; en1.MoveNext() && en2.MoveNext() ;)
                    yield return (en1.Current, en2.Current);
            }

            Guard.AssertArgumentNotNull(src1, nameof(src1));
            Guard.AssertArgumentNotNull(src2, nameof(src2));
            return core();
        }


        /// <summary>
        ///     Zips 2 sequences to tuple sequence.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <returns></returns>
        internal static IEnumerable<(T1, T2)> ZipLeft<T1, T2>(
            this IEnumerable<T1> src1, IEnumerable<T2> src2)
        {
            IEnumerable<(T1, T2)> core()
            {
                var en1 = src1.GetEnumerator();
                var en2 = src2.GetEnumerator();
                for(; en1.MoveNext() ;)
                    yield return (en1.Current,
                                  en2.MoveNext() ? en2.Current : default);
            }

            Guard.AssertArgumentNotNull(src1, nameof(src1));
            Guard.AssertArgumentNotNull(src2, nameof(src2));
            return core();
        }


        /// <summary>
        ///     Zips 2 sequences to tuple sequence.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <returns></returns>
        internal static IEnumerable<(T1, T2)> ZipOuter<T1, T2>(
            this IEnumerable<T1> src1, IEnumerable<T2> src2)
        {
            IEnumerable<(T1, T2)> core()
            {
                var en1 = src1.GetEnumerator();
                var en2 = src2.GetEnumerator();
                while(true)
                {
                    var en1Curr = en1.MoveNext();
                    var en2Curr = en2.MoveNext();
                    if(!en1Curr && !en2Curr)
                        break;
                    yield return (en1Curr ? en1.Current : default,
                                  en2Curr ? en2.Current : default);
                }
            }

            Guard.AssertArgumentNotNull(src1, nameof(src1));
            Guard.AssertArgumentNotNull(src2, nameof(src2));
            return core();
        }


        /// <summary>
        ///     Projects each element into a new form, 
        ///     but the first element will be projected with special selector.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="topSelector"></param>
        /// <param name="othersSelector"></param>
        /// <returns></returns>
        internal static IEnumerable<TResult> SelectTop<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> topSelector,
            Func<TSource, TResult> othersSelector)
        {
            IEnumerable<TResult> core()
            {
                var iter = source.GetEnumerator();
                if(!iter.MoveNext())
                    yield break;
                yield return topSelector(iter.Current);
                while(iter.MoveNext())
                    yield return othersSelector(iter.Current);
            }

            Guard.AssertArgumentNotNull(source, nameof(source));
            Guard.AssertArgumentNotNull(topSelector, nameof(topSelector));
            Guard.AssertArgumentNotNull(othersSelector, nameof(othersSelector));
            return core();
        }


        /// <summary>
        ///     Projects each element into a new form, 
        ///     but the last element will be projected with special selector.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="othersSelector"></param>
        /// <param name="tailSelector"></param>
        /// <returns></returns>
        internal static IEnumerable<TResult> SelectTail<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> othersSelector,
            Func<TSource, TResult> tailSelector)
        {
            IEnumerable<TResult> core()
            {
                var iter = source.GetEnumerator();
                if(!iter.MoveNext())
                    yield break;
                var cache = iter.Current;
                while(iter.MoveNext())
                {
                    yield return othersSelector(cache);
                    cache = iter.Current;
                }

                yield return tailSelector(cache);
            }

            Guard.AssertArgumentNotNull(source, nameof(source));
            Guard.AssertArgumentNotNull(tailSelector, nameof(tailSelector));
            Guard.AssertArgumentNotNull(othersSelector, nameof(othersSelector));
            return core();
        }

    }
}
