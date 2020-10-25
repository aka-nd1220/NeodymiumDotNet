using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    using PyTupleObject = PyObject<IReadOnlyList<IPyObject>>;

    /// <summary>
    ///     Python tuple object parser.
    /// </summary>
    internal static class PyTuple
    {

        public static readonly Parser<PyTupleObject> Tuple
            = from open in Parse.Char('(').MakePositioned()
              from items in PyMultiObject.GetCommaChainedItems(PyObject.Object, true, false)
              from close in Parse.Char(')').MakePositioned()
              select new PyTupleObject(items.ToList(),
                                       open.StartInInput,
                                       close.EndInInput - open.StartInInput);


        /// <summary>
        ///     Unwraps from <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="pyTuple"></param>
        /// <returns></returns>
        public static IReadOnlyList<object> UnPy(this PyTupleObject pyTuple)
            => pyTuple.Value.Select(x => x.Value).ToList();


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static PyTupleObject EnPy(IReadOnlyList<object> tuple)
            => new PyTupleObject(tuple.Select(PyObject.EnPy).ToList(), -1, -1);


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static PyTupleObject EnPy(IReadOnlyList<IPyObject> tuple)
            => new PyTupleObject(tuple.ToList(), -1, -1);

    }
}
