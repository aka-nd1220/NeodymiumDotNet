using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    using PyListObject = PyObject<IList<IPyObject>>;

    /// <summary>
    ///     Python list object parser.
    /// </summary>
    internal static class PyList
    {

        public static readonly Parser<PyListObject> List
            = from open in Parse.Char('[').MakePositioned()
              from items in PyMultiObject.GetCommaChainedItems(PyObject.Object, false, true)
              from close in Parse.Char(']').MakePositioned()
              select new PyListObject(items.ToList(),
                                      open.StartInInput,
                                      close.EndInInput - open.StartInInput);


        /// <summary>
        ///     Unwraps from <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="pyList"></param>
        /// <returns></returns>
        public static IList<object> UnPy(this PyListObject pyList)
            => pyList.Value.Select(x => x.Value).ToList();


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PyListObject EnPy(IList<object> list)
            => new PyListObject(list.Select(PyObject.EnPy).ToList(), -1, -1);


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PyListObject EnPy(IList<IPyObject> list)
            => new PyListObject(list.ToList(), -1, -1);

    }
}
