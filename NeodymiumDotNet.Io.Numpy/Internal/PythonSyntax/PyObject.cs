using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    /// <summary>
    ///     Presents non-typed python object base type.
    /// </summary>
    interface IPyObject
    {

        int StartInSource { get; }

        int LengthInSource { get; }

        object Value { get; }

    }

    /// <summary>
    ///     Presents generic-typed python object base type.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal interface IPyObject<out TValue> : IPyObject
    {

        new TValue Value { get; }

    }

    /// <summary>
    ///     Presents python object base class.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal class PyObject<TValue> : IPyObject<TValue>
    {

        public int StartInSource { get; }

        public int LengthInSource { get; }

        public TValue Value { get; }

        object IPyObject.Value => Value;


        public PyObject(TValue value, int startInSource, int lengthInSource)
        {
            Value = value;
            StartInSource = startInSource;
            LengthInSource = lengthInSource;
        }


        public override int GetHashCode() => Value.GetHashCode();


        public override bool Equals(object obj)
            => PyObjectComparer.Instance.Equals(this, obj);


        public static implicit operator PyObject<TValue>(TValue value)
            => new PyObject<TValue>(value, -1, -1);

    }

    /// <summary>
    ///     Python generic object parser definitions.
    /// </summary>
    internal static class PyObject
    {

        #region parser

        public static readonly Parser<IPyObject> Object
            = ParserUtils.Or<IPyObject>(PyBoolean.Boolean.Box(),
                                        PyString.StringLiteral,
                                        PyImaginary.ImaginaryLiteral.Box(),
                                        PyFloat.FloatLiteral.Box(),
                                        PyInteger.IntegerLiteral.Box(),
                                        PyTuple.Tuple,
                                        PyList.List,
                                        PyDict.Dict);

        #endregion


        public static Parser<PyObject<T>> MatchPerfectly<T>(this Parser<PyObject<T>> parser)
        {
            return input =>
            {
                var result = parser(input);
                if(!result.WasSuccessful)
                    return result;
                if(result.Value.StartInSource != 0 || !result.Remainder.AtEnd)
                    return Result.Failure<PyObject<T>>(result.Remainder,
                                                       "Input is not matched perfectly with pattern.",
                                                       Enumerable.Empty<string>());
                return result;
            };
        }


        public static Parser<PyObject<object>> Box<T>(this Parser<IPyObject<T>> parser)
            where T : struct
            => parser.Select(x => new PyObject<object>(x.Value, x.StartInSource,
                                                       x.LengthInSource));


        /// <summary>
        ///     Unwraps from <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="pyObj"></param>
        /// <returns></returns>
        public static object UnPy(this IPyObject pyObj)
        {
            switch(pyObj)
            {
            case PyObject<IDictionary<IPyObject, IPyObject>> dict:
                return PyDict.UnPy(dict);
            case PyObject<IReadOnlyList<IPyObject>> tuple:
                return PyTuple.UnPy(tuple);
            case PyObject<IList<IPyObject>> list:
                return PyList.UnPy(list);
            default:
                return pyObj.Value;
            }
        }


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IPyObject EnPy(object obj)
        {
            switch(obj)
            {
            case IDictionary<object, object> dict:
                return PyDict.EnPy(dict);
            case IReadOnlyList<object> tuple:
                return PyTuple.EnPy(tuple);
            case IList<object> list:
                return PyList.EnPy(list);
            default:
                return (PyObject<object>)obj;
            }
        }

    }
}
