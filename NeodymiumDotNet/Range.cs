using System;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     The range of number-line.
    ///     This range enumerate <c>Start</c> to <c>End - 1</c>.
    /// </summary>
    public readonly struct Range
    {
        /// <summary>
        ///     Gets the <see cref="Range"/> instance which means empty range.
        /// </summary>
        public static Range Empty => Create(0);


        /// <summary>
        ///     Gets the <see cref="Range"/> instance which means whole range.
        /// </summary>
        public static Range Whole => new Range(0, new Index(0, true), 1);


        /// <summary>
        ///     Gets the start position.
        /// </summary>
        public Index Start { get; }


        /// <summary>
        ///     Gets the end position.
        /// </summary>
        public Index End { get; }


        /// <summary>
        ///     [<c>Step &gt; 0</c>] Gets the iteration step.
        /// </summary>
        public int Step { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"> [<c>0 &gt; step</c>] </param>
        public Range(Index start, Index end, int step)
        {
            Guard.AssertArgumentRange(step > 0, "step must be greater than 0.");

            Start = start;
            End   = end;
            Step = step;
        }


        /// <summary>
        ///     Maps the relative index to base number-line.
        /// </summary>
        /// <param name="relIndex"> The relative index on this range. </param>
        /// <param name="length"> The length of base number-line. </param>
        /// <returns></returns>
        public int Map(int relIndex, int length)
        {
            var start = Start.Map(length);
            var end = End.Map(length);
            var retval = start + Step * relIndex;
            Guard.AssertArgumentRange(retval < end, "retval must be less than end.");
            return retval;
        }


        /// <summary>
        ///     Maps this range length to base number-line.
        /// </summary>
        /// <param name="length"> The length of base number-line. </param>
        /// <returns></returns>
        public int MapLength(int length)
        {
            var start = Start.Map(length);
            var end = End.Map(length);
            return Ceiling(end - start, Step);
        }


        /// <summary>
        ///     Returns the string representation of the current Range object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"Range({Start}, {End}, {Step})";


        /// <summary>
        ///     [Pure] Creates a new <see cref="Range"/>.
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Range Create(int end)
            => new Range(0, end, 1);


        /// <summary>
        ///     [Pure] Creates a new <see cref="Range"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Range Create(int start, int end)
            => new Range(start, end, 1);


        /// <summary>
        ///     [Pure] Creates a new <see cref="Range"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static Range Create(int start, int end, int step)
            => new Range(start, end, step);


        /// <summary>
        ///     [Pure] Calculate ceiling of <see cref="Int32"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static int Ceiling(int x, int y)
            => (x / y) + (x % y != 0 ? 1 : 0);

    }
}
