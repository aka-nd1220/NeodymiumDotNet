using System;
using System.Collections.Generic;
using System.Text;
using NeodymiumDotNet.Io.Numpy.PythonSyntax;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy
{
    /// <summary>
    ///     Presents the object which were parsed the source text and its position in the source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SourcePositionedObject<T> : IPositionAware<SourcePositionedObject<T>>
    {

        private Position _StartPos;

        public int StartInInput => _StartPos.Pos;

        public int LengthInInput { get; private set; }

        public int EndInInput => StartInInput + LengthInInput;

        public T Value { get; }


        public SourcePositionedObject(T value)
        {
            Value = value;
            _StartPos = new Position(0, 1, 1);
            LengthInInput = 0;
        }


        public SourcePositionedObject<T> SetPos(Position startPos, int length)
        {
            _StartPos = startPos;
            LengthInInput = length;
            return this;
        }

    }

    internal static class SourcePositionedObject
    {

        public static SourcePositionedObject<T> Create<T>(T value)
            => new SourcePositionedObject<T>(value);


        public static Parser<SourcePositionedObject<T>> MakePositioned<T>(this Parser<T> parser)
            => parser.Select(Create).Positioned();

    }
}
