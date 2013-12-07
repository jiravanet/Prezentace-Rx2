using System;

namespace Rx
{
    public class ConsoleObserver : IObserver<int>
    {
        readonly string prefix;

        public ConsoleObserver()
            : this(String.Empty)
        {}

        public ConsoleObserver(string prefix)
        {
            this.prefix = prefix;
        }

        public void OnNext(int value)
        {
            Console.WriteLine(prefix + value);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(prefix + error.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine(prefix + "Completed");
        }
    }
}