using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    using PyDictObject = PyObject<IDictionary<IPyObject, IPyObject>>;

    /// <summary>
    ///     Python dictionary object parser.
    /// </summary>
    internal static class PyDict
    {

        private static readonly Parser<(IPyObject key, IPyObject value)> _KeyValuePair
            = from space1 in Parse.WhiteSpace.Many()
              from key in PyObject.Object
              from space2 in Parse.WhiteSpace.Many()
              from separator in Parse.Char(':')
              from space3 in Parse.WhiteSpace.Many()
              from value in PyObject.Object
              from space4 in Parse.WhiteSpace.Many()
              select (key, value);


        public static readonly Parser<PyDictObject> Dict
            = from open in Parse.Char('{').MakePositioned()
              from items in PyMultiObject.GetCommaChainedItems(_KeyValuePair, false, true)
              from close in Parse.Char('}').MakePositioned()
              select new PyDictObject(items.ToDictionary(tpl => tpl.key, tpl => tpl.value),
                                      open.StartInInput,
                                      close.EndInInput - open.StartInInput);


        /// <summary>
        ///     Unwraps from <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="pyDict"></param>
        /// <returns></returns>
        public static IDictionary<object, object> UnPy(this PyDictObject pyDict)
            => pyDict.Value.ToDictionary(kv => kv.Key.UnPy(), kv => kv.Value.UnPy());


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static PyDictObject EnPy(IDictionary<object, object> dict)
        {
            var pyDict =
                dict.ToDictionary(kv => PyObject.EnPy(kv.Key), kv => PyObject.EnPy(kv.Value));
            return new PyDictObject(pyDict, -1, -1);
        }


        /// <summary>
        ///     Wraps in <see cref="IPyObject"/> wrapper.
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static PyDictObject EnPy(IDictionary<IPyObject, IPyObject> dict)
            => new PyDictObject(new Dictionary<IPyObject, IPyObject>(dict), -1, -1);

    }
}
