using static System.Console;

class Program
{

    static void Main(string[] args)
    {
        ExecuteSample(new InstantiateSample());
        ExecuteSample(new IndexAccessSample());
        ExecuteSample(new LinqSample());
        ReadKey();
    }


    static void ExecuteSample(ISample sample)
    {
        WriteLine(new string('=', 80));
        WriteLine((sample.GetType().Name + " ").PadRight(80, '='));
        WriteLine(new string('=', 80));
        sample.Execute();
        WriteLine();
    }

}
