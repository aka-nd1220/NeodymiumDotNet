using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeodymiumDotNet
{
    internal static class Guard
    {
        [DebuggerHidden]
        public static void ThrowNotSupport()
            => throw new NotSupportedException();

        [DebuggerHidden]
        public static void ThrowInvalidCast(string message)
            => throw new InvalidCastException(message);


        [DebuggerHidden]
        public static void AssertCast(bool requiredCondition, string message)
        {
            if(!requiredCondition)
                ThrowInvalidCast(message);
        }


        public static void ThrowArgumentNull(string argName)
            => throw new ArgumentNullException(argName);

        [DebuggerHidden]
        public static void AssertArgumentNotNull<T>(T value, string argName)
            where T : class
        {
            if(value is null)
                ThrowArgumentNull(argName);
        }


        [DebuggerHidden]
        public static void ThrowArgumentError(string message)
            => throw new ArgumentException(message);

        [DebuggerHidden]
        public static void AssertArgument(bool requiredCondition, string message)
        {
            if(!requiredCondition)
                ThrowArgumentError(message);
        }


        [DebuggerHidden]
        public static void ThrowFormatError()
            => throw new FormatException();

        [DebuggerHidden]
        public static void AssertFormat(bool requiredCondition)
        {
            if(!requiredCondition)
                ThrowFormatError();
        }


        [DebuggerHidden]
        public static void ThrowArgumentOutOfRange(string message)
            => throw new ArgumentOutOfRangeException(message);

        [DebuggerHidden]
        public static void AssertArgumentRange(bool requiredCondition, string message)
        {
            if(!requiredCondition)
                ThrowArgumentOutOfRange(message);
        }


        [DebuggerHidden]
        public static void ThrowInvalidOperation(string message)
            => throw new InvalidOperationException(message);

        [DebuggerHidden]
        public static void AssertOperation(bool requiredCondition, string message)
        {
            if(!requiredCondition)
                ThrowInvalidOperation(message);
        }


        [DebuggerHidden]
        public static void AssertIndices(IndexArray shape, ReadOnlySpan<int> indices)
        {
            if(shape.Length != indices.Length)
                ThrowArgumentOutOfRange($"Shape={shape}, indices={new IndexArray(indices.ToArray())}");
            for(int i = 0, len = shape.Length ; i < len ; ++i)
                if((uint)indices[i] >= (uint)shape[i])
                    ThrowArgumentOutOfRange($"Shape={shape}, indices={new IndexArray(indices.ToArray())}");
        }


        [DebuggerHidden]
        public static void AssertIndices(IndexArray shape, IndexOrRange[] indices)
        {
            void throwIfFailed(bool requiredCondition) =>
                AssertArgumentRange(requiredCondition, $"Shape={shape}, indices={shape}");

            throwIfFailed(shape.Length == indices.Length);
            for(int i = 0, len = shape.Length ; i < len ; ++i)
            {
                var index = indices[i];
                if(index.IsRange)
                {
                    var start = index.Range.Map(0, shape[i]);
                    throwIfFailed((uint)start < (uint)shape[i]);
                    var end = index.Range.Map(index.Range.MapLength(shape[i]) - 1, shape[i]);
                    throwIfFailed((uint)end <= (uint)shape[i]);
                }
                else throwIfFailed((uint)index.Index.Value < (uint)shape[i]);
            }
        }


        [DebuggerHidden]
        public static void ThrowShapeMismatch(string message)
            => throw new ShapeMismatchException(message);

        [DebuggerHidden]
        public static void AssertShapeMatch(bool requiredCondition, string message)
        {
            if(!requiredCondition)
                ThrowShapeMismatch(message);
        }

        [DebuggerHidden]
        public static void AssertShapeMatch(IndexArray expected, IndexArray actual,  string argName)
        {
            if(expected != actual)
                ThrowShapeMismatch($"NdArray shapes of the arguments were mismatched. (expected={expected}, {argName}={actual})");
        }

        [DebuggerHidden]
        public static void AssertShapeMatch(IndexArray xShape, IndexArray yShape, string xArgName, string yArgName)
        {
            if(xShape != yShape)
                ThrowShapeMismatch($"NdArray shapes of the arguments were mismatched. ({xArgName}={xShape}, {yArgName}={yShape})");
        }

    }
}
