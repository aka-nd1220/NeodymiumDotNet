using System;
using System.Collections.Generic;
using System.Text;

namespace NeodymiumDotNet.Io.Numpy.PythonSyntax
{
    internal class PyObjectComparer : IEqualityComparer<object>
    {

        public static readonly IEqualityComparer<object> Instance
            = new PyObjectComparer();


        public new bool Equals(object x, object y)
            => (dynamic)RemoveWrap(x) == (dynamic)RemoveWrap(y);


        public int GetHashCode(object obj)
            => RemoveWrap(obj).GetHashCode();


        private static object RemoveWrap(object value)
            => value is IPyObject tmp1 ? tmp1.Value : value;

    }
}
