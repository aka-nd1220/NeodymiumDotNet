using NeodymiumDotNet;
using static System.Console;

public class InstantiateSample : ISample
{

    public void Execute()
    {
        // Creates with 1-D array and shape definition.
        var ndarray1 = NdArray.Create(new double[24], new int[] { 2, 3, 4 });
        WriteLine(ndarray1);

        // Creates with multi-dimension array.
        var ndarray2 = NdArray.Create(new double[2, 3, 4]);
        WriteLine(ndarray2);

        // Basic NdArray is immutable.
        // If you need mutable NdArray, use `CreateMutable` instead of `Create`.
        var ndarray3 = NdArray.CreateMutable(new double[2, 3, 4]);
        WriteLine(ndarray3);

        // You can convert mutable <-> immutable NdArray with `ToImmutable`/`ToMutable`.
        // These methods create copy.
        var ndarray4 = ndarray3.ToImmutable();
        WriteLine(ndarray4);
        var ndarray5 = ndarray1.ToMutable();
        WriteLine(ndarray5);

        // You can also convert mutable -> immutable with `MoveTommutable`.
        // This method moves internal buffer, but does not create copy.
        // Please note this method destroys the source mutable NdArray.
        var ndarray6 = ndarray3.MoveToImmutable();
        WriteLine(ndarray6);

        // If generic data type T has `0`/`1` value, you can use `Zeros`/`Ones`.
        var ndarray7 = NdArray.Zeros<double>(new int[] { 2, 3, 4 });
        WriteLine(ndarray7);
        var ndarray8 = NdArray.Ones<double>(new int[] { 2, 3, 4 });
        WriteLine(ndarray8);
    }

}
