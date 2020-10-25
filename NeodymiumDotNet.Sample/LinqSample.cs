using NeodymiumDotNet;
using NeodymiumDotNet.Linq;
using NeodymiumDotNet.Statistics;
using static System.Console;

public class LinqSample : ISample
{

    public void Execute()
    {
        // These operator is provided in `NeodymiumDotNet.Linq`.
        // They are not operators of LINQ to Objects.

        var source1 = NdArray.Create(new int[,,]
        {
            { { 0,  1,  2,  3 }, { 4,  5,  6,  7 }, { 8,  9, 10, 11 } },
            { { 12, 13, 14, 15 }, { 16, 17, 18, 19 }, { 20, 21, 22, 23 } }
        });
        var source2 = NdArray.Create(new int[,,]
        {
            { { 0,  -1,  2,  -3 }, { 4,  -5,  6,  -7 }, { 8,  -9, 10, -11 } },
            { { 12, -13, 14, -15 }, { 16, -17, 18, -19 }, { 20, -21, 22, -23 } }
        });

        // You can project each element with `Select`.
        var result1 = source1.Select(x => x * 3);
        WriteLine(result1);
        /*  result1 = NdArray({{{ 0,  3,  6,  9},
         *                      {12, 15, 18, 21},
         *                      {24, 27, 30, 33}},
         *                     {{36, 39, 42, 45},
         *                      {48, 51, 54, 57},
         *                      {60, 63, 66, 69}}})
         */

        // You can also project axes-based partial array with `Select`.
        var result2 = source1.Select(new[] { 0 }, x => x.Sum());
        WriteLine(result2);
        /*  x = NdArray({ 0, 12,}),
         *      NdArray({ 1, 13,}),
         *      ...,
         *      NdArray({11, 23,})
         *  result2 = NdArray({{12, 14, 16, 18},
         *                     {20, 22, 24, 26},
         *                     {28, 30, 32, 34}})
         */
        var result3 = source1.Select(new[] { 0, 1 }, x => x.Sum());
        WriteLine(result3);
        /*  x = NdArray({{ 0,  4,  8}, {12, 16, 20}}),
         *      NdArray({{ 1,  5,  9}, {13, 17, 21}}),
         *      NdArray({{ 2,  6, 10}, {14, 18, 22}}),
         *      NdArray({{ 3,  7, 11}, {15, 19, 23}})
         *  result3 = NdArray({60, 66, 72, 78})
         */

        // If you want to apply calculation for each elements of 2 or more NdArrays,
        // please use `Zip`.
        var result4 = source1.Zip(source2, (x, y) => x + y);
        WriteLine(result4);
        /*  result4 = NdArray({{{ 0,  0,  4,  0},
         *                      { 8,  0, 12,  0},
         *                      {16,  0, 20,  0}},
         *                     {{24,  0, 28,  0},
         *                      {32,  0, 36,  0},
         *                      {40,  0, 44,  0}}})
         */
        // You can also code following expression.
        var result4_alt = (source1, source2).Zip((x, y) => x + y);
            
        // You can filter along an axis with `Where`.
        // NOTE: `Where` operator needs a single axis due to maintain rank between source and result.
        var result5 = source1.Where(filterAxis: 0, x => x.Sum() < 70);
        WriteLine(result5);
        /*  x = NdArray({{  0,  1,  2,  3}, { 4,  5,  6,  7}, { 8,  9, 10, 11}}),
         *      NdArray({{ 12, 13, 14, 15}, {16, 17, 18, 19}, {20, 21, 22, 23}})
         *  result5 = NdArray({{{  0,  1,  2,  3}, { 4,  5,  6,  7}, { 8,  9, 10, 11}}})
         */
        var result6 = source1.Where(filterAxis: 2, x => x.Sum() % 12 == 0);
        WriteLine(result6);
        /*  x = NdArray({{ 0,  4,  8}, {12, 16, 20}}),
         *      NdArray({{ 1,  5,  9}, {13, 17, 21}}),
         *      NdArray({{ 2,  6, 10}, {14, 18, 22}}),
         *      NdArray({{ 3,  7, 11}, {15, 19, 23}})
         *  result6 = NdArray({{{ 0,  2},
         *                      { 4,  6},
         *                      { 8, 10}},
         *                     {{12, 14},
         *                      {16, 18},
         *                      {20, 22}}})
         */
    }

}
