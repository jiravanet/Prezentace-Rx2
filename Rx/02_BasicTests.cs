using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using Xunit;

namespace Rx
{
    public class BasicTests
    {
        [Fact]
        public void ShouldEmpty()
        {
            var sequence = Observable.Empty<int>();
            sequence.Subscribe(new ConsoleObserver());
            Console.WriteLine("Konec");
        }

        [Fact]
        public void ShouldReturn()
        {
            var sequence = Observable.Return(42);
            sequence.Subscribe(new ConsoleObserver());
            Console.WriteLine("Konec");
        }

        [Fact]
        public void ShouldNever()
        {
            var sequence = Observable.Never<int>();
            sequence.Subscribe(new ConsoleObserver());
            Console.WriteLine("Konec");
        }

        [Fact]
        public void ShouldRepeat()
        {
            var sequence = Observable.Repeat(42,
                NewThreadScheduler.Default);
            using (sequence.Subscribe(new ConsoleObserver()))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(20));
            }
        }

        [Fact]
        public void ShouldTimestampValue()
        {
            var sequence = (from i in new[] { 3, 6, 8, 2, 9 }
                           select WaitNumber(i)).
                           ToObservable().
                           Timestamp();
            sequence.Subscribe(ti => Console.WriteLine("Number {0} @ {1:mm:ss:fff}",
                ti.Value,
                ti.Timestamp));
        }

        int WaitNumber(int number)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(120));
            return number;
        }
    }
}