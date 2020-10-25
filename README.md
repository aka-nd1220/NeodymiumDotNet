# NeodymiumDotNet

High speed multi-dimensional array library implemented with pure C#

## Description

While being inspired by [numpy](http://www.numpy.org/), NeodymiumDotNet is
designed to maximize the benefits of .net.

Any dimensional array which has various operation is supported.

All of public members are CLS compliant.

### Design Philosophy

NeodymiumDotNet is designed to ...

- **be preventive against performance degradation**

  For example, `NdArray<T>` does not implement `IEnumerable<T>` interface due to
  suppress unintended Linq to Object, and Linq to NdArray is provided specially.

- **achieve both safety and flexibility**

  For data processing, it is considered preferable that data types are
  implemented as immutable.
  In other hand, because of data size, sometimes it is efficient to edit mutable
  data object.

  NeodymiumDotNet takes immutable NdArray as basic, and also support mutable
  NdArray. Available operations are explicitly defined according to property of
  each NdArray.

### Supporting file format

- `.npy`, numpy archive file

## License

NeodymiumDotNet is licensed under the [Apache License v2.0](/LICENSE.txt).

## Requirement

This project targets .net standard 2.0.

This project contains outcomes of following projects.

- [corefx libraries](https://github.com/dotnet/corefx)

  ... distributed under MIT license.

  - System.Memory

  - System.Runtime.CompilerServices.Unsafe

- [Microsoft.CSharp](https://www.microsoft.com/net)

  ... distributed under MIT license.

- [xUnit.Net](https://xunit.github.io/)

  ... distributed under Apache license v2.0.

- [Sprache](https://github.com/sprache/Sprache)

  ... distributed under MIT license.

## Install

This project is in progress as beta version, and there is no release version yet.

## Usage

### Creates NdArray instance

```CSharp
using NeodymiumDotNet;

public class InstantiateSample : ISample
{
    public void Execute()
    {
        // Creates with 1-D array and shape definition.
        var ndarray1 = NdArray.Create(new double[24], new int[]{2, 3, 4});

        // Creates with multi-dimension array.
        var ndarray2 = NdArray.Create(new double[2, 3, 4]);

        // Basic NdArray is immutable.
        // If you need mutable NdArray, use `CreateMutable` instead of `Create`.
        var ndarray3 = NdArray.CreateMutable(new double[2, 3, 4]);

        // You can convert mutable <-> immutable NdArray with `ToImmutable`/`ToMutable`.
        // These methods create copy.
        var ndarray4 = ndarray3.ToImmutable();
        var ndarray5 = ndarray1.ToMutable();

        // You can also convert mutable -> immutable with `MoveToImmutable`.
        // This method moves internal buffer, but does not create copy.
        // Please note this method destroys the source mutable NdArray.
        var ndarray6 = ndarray3.MoveToImmutable();

        // If generic data type T has `0`/`1` value, you can use `Zeros`/`Ones`.
        var ndarray7 = NdArray.Zeros<double>(new int[]{2, 3, 4});
        var ndarray8 = NdArray.Ones<double>(new int[]{2, 3, 4});
    }
}

```

### Index access

```CSharp
using NeodymiumDotNet;
using static System.Console;

public class IndexAccessSample : ISample
{
    public void Execute()
    {
        var source = NdArray.Create(new double[,,]
        {
            {{ 0.0,  1.0,  2.0,  3.0}, { 4.0,  5.0,  6.0,  7.0}, { 8.0,  9.0, 10.0, 11.0}},
            {{12.0, 13.0, 14.0, 15.0}, {16.0, 17.0, 18.0, 19.0}, {20.0, 21.0, 22.0, 23.0}}
        });

        WriteLine(source[0, 0, 0]); // 0.0
        WriteLine(source[0, 0, 3]); // 3.0
        WriteLine(source[0, 2, 0]); // 8.0
        WriteLine(source[1, 0, 0]); // 12.0
        WriteLine(source[1, 2, 3]); // 23.0
        WriteLine(source.GetByFlattenIndex(0));  // 0.0
        WriteLine(source.GetByFlattenIndex(10)); // 10.0
        WriteLine(source.GetByFlattenIndex(20)); // 20.0
    }
}
```

### LINQ to NdArray

```CSharp
using NeodymiumDotNet;
using NeodymiumDotNet.Linq;
using static System.Console;

public class LinqSample : ISample
{
    public void Execute()
    {
        var source1 = NdArray.Create(new int[,,]
        {
            {{ 0,  1,  2,  3}, { 4,  5,  6,  7}, { 8,  9, 10, 11}},
            {{12, 13, 14, 15}, {16, 17, 18, 19}, {20, 21, 22, 23}}
        });
        var source2 = NdArray.Create(new int[,,]
        {
            {{ 0,  -1,  2,  -3}, { 4,  -5,  6,  -7}, { 8,  -9, 10, -11}},
            {{12, -13, 14, -15}, {16, -17, 18, -19}, {20, -21, 22, -23}}
        });

        // You can project each element with `Select`.
        var result1 = source1.Select(x => x * 3);
        WriteLine(result1);
        /*  NdArray.Create(new int[,,]
         *  {
         *      {{ 0,  3,  6,  9}, {12, 15, 18, 21}, {24, 27, 30, 33}},
         *      {{36, 39, 42, 45}, {48, 51, 54, 57}, {60, 63, 66, 69}}
         *  });
         */

        // You can also project axes-based partial array with `Select`.
        var result2 = source1.Select(new[] { 0 }, x => x[1] / 2.0);
        WriteLine(result2);
        /*  NdArray.Create(new double[,]
         *  {
         *      {{6.0, 6.5, 7.0, 7.5}, {8.0, 8.5, 9.0, 9.5}, {10.0, 10.5, 11.0, 11.5}}
         *  });
         */

        // If you want to apply calculation for each elements of 2 or more NdArrays,
        // please use `Zip`.
        var result3 = source1.Zip(source2, (x, y) => x + y);
        WriteLine(result3);
        /*  NdArray.Create(new int[,,]
         *  {
         *      {{ 0,  0,  4,  0}, { 8,  0, 12,  0}, {16,  0, 20,  0}},
         *      {{24,  0, 28,  0}, {32,  0, 36,  0}, {40,  0, 44,  0}}
         *  });
         */
    }
}
```

### Statistics

```CSharp
using NeodymiumDotNet;
using NeodymiumDotNet.Linq;
using NeodymiumDotNet.Statistics;

public static class Foo
{
    public static void Bar()
    {
        var source = NdArray.Create(new double[,,]
        {
            {{ 0.0,  1.0,  2.0,  3.0}, { 4.0,  5.0,  6.0,  7.0}, { 8.0,  9.0, 10.0, 11.0}},
            {{12.0, 13.0, 14.0, 15.0}, {16.0, 17.0, 18.0, 19.0}, {20.0, 21.0, 22.0, 23.0}}
        });

        // Statistics function is defined in `NeodymiumDotNet.Statistics`.
        var result1 = source.Sum();
        // is equals to 

        // If you want to employ some axes along which the means are computed,
        // please use `Select`.
        var result2 = source.Select(axes: new []{0}, x => x.Mean());
    }
}
```

## Author

[GlassGrass](https://github.com/GlassGrass)
