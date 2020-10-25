using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Represents a evaluation strategy of iteration.
    /// </summary>
    public interface IIterationStrategy
    {
        /// <summary>
        ///     Executes a for loop in which iterations may run in the strategy.
        /// </summary>
        /// <param name="fromInclusive"></param>
        /// <param name="toExclusive"></param>
        /// <param name="body"></param>
        void For(int fromInclusive, int toExclusive, Action<int> body);
    }


    /// <summary>
    ///     Provides default implementation of <see cref="IIterationStrategy"/>.
    /// </summary>
    public sealed class IterationStrategy : IIterationStrategy
    {
        /// <summary>
        ///     Gets default iteration strategy.
        /// </summary>
        public static IIterationStrategy Default { get; } = new IterationStrategy();


        private IterationStrategy()
        {
        }


        /// <inheritdoc />
        public void For(int fromInclusive, int toExclusive, Action<int> body)
        {
            for(var i = fromInclusive; i < toExclusive; ++i)
                body(i);
        }
    }
}
