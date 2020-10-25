using System.Collections.Generic;
using NeodymiumDotNet;
using static System.Console;

public class IndexAccessSample : ISample
{

    public void Execute()
    {
        var source = NdArray.Create(new double[,,]
        {
            { { 0.0,  1.0,  2.0,  3.0 }, { 4.0,  5.0,  6.0,  7.0 }, { 8.0,  9.0, 10.0, 11.0 } },
            {
                { 12.0, 13.0, 14.0, 15.0 }, { 16.0, 17.0, 18.0, 19.0 },
                { 20.0, 21.0, 22.0, 23.0 }
            }
        });

        WriteLine(source[0, 0, 0]);              // 0.0
        WriteLine(source[0, 0, 3]);              // 3.0
        WriteLine(source[0, 2, 0]);              // 8.0
        WriteLine(source[1, 0, 0]);              // 12.0
        WriteLine(source[1, 2, 3]);              // 23.0
        WriteLine(source.GetItem(0));  // 0.0
        WriteLine(source.GetItem(10)); // 10.0
        WriteLine(source.GetItem(20)); // 20.0

        // `NdArray<T>` is available in foreach statement.
        // But it is not `IEnumerable < T >` to prevent unintended use of LINQ to Objects.
        // If you want to handle `NdArray<T>` as `IEnumerable<T>` explicitly,
        // please use `AsEnumerable()`.
        foreach(var x in source)
        {
            WriteLine(x);
        }

        WriteLine(source is IEnumerable<double>);                // false
        WriteLine(source.AsEnumerable() is IEnumerable<double>); // true

        // You can also use `AsEnumerable(int axis)` to iterate along specified axis.
        foreach(var x in source.AsEnumerable(1))
        {
            WriteLine(x);
        }

        /*  NdArray({{ 0,  1,  2,  3},
         *           {12, 13, 14, 15}})
         *  NdArray({{ 4,  5,  6,  7},
         *           {16, 17, 18, 19}})
         *  NdArray({{ 8,  9, 10, 11},
         *           {20, 21, 22, 23}})
         */
        WriteLine(source.AsEnumerable(1) is IEnumerable<NdArray<double>>); // true
    }

}
