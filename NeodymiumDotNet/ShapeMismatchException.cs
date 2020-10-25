using System;
using System.Collections.Generic;

namespace NeodymiumDotNet
{
    /// <summary>
    ///     Means the <see cref="NdArray{T}"/> argument shape is mismatched.
    /// </summary>
    public class ShapeMismatchException : ArgumentException
    {
        /// <summary>
        ///     Creates a new instance.
        /// </summary>
        /// <param name="message"></param>
        public ShapeMismatchException(string message)
            : base(message)
        {
        }
    }
}
