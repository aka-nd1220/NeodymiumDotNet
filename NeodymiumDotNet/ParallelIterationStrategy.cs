using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Provides parallelized implementation of <see cref="IterationStrategy"/>.
    /// </summary>
    public class ParallelIterationStrategy : IIterationStrategy
    {
        /// <summary>
        ///     Gets singleton instance of <see cref="ParallelIterationStrategy"/>.
        /// </summary>
        public static IIterationStrategy Instance { get; } = new ParallelIterationStrategy();


        private ParallelIterationStrategy()
        {
        }


        /// <inheritdoc />
        public void For(int fromInclusive, int toExclusive, Action<int> body)
            => Parallel.For(fromInclusive, toExclusive, body);
    }
}
