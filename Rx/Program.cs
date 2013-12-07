using System;
using System.Linq;
using System.Reactive.Linq;

namespace Rx
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = Enumerable.Range(1, 10);
            foreach (var number in query)
                PrintNumber(number);

            Done();

            var observable = Observable.Range(1, 10);
            observable.Subscribe(PrintNumber, Done);

            Console.ReadKey();
        }

        static void PrintNumber(int number)
        {
            Console.WriteLine(number);
        }

        static void Done()
        {
            Console.WriteLine("Done");
        }

        static void Error(Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
}
