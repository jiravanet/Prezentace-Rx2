using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using Xunit;

namespace Rx
{
    public class ErrorTests
    {
        [Fact]
        public void ShouldStopWhenException()
        {
            var done = new ManualResetEvent(false);
            var query = from number in Enumerable.Range(0, 10) select number;
            var sequence = query.ToObservable(NewThreadScheduler.Default);
            sequence
                .Select(number => number / (number - 5))
                .Finally(() => done.Set())
                .Subscribe(new ConsoleObserver());
            done.WaitOne();

        }
        [Fact]
        public void ShouldResumeFromError()
        {
            var done = new ManualResetEvent(false);
            var query = from number in Enumerable.Range(0, 10) select number;
            var sequence = query.ToObservable(NewThreadScheduler.Default);
            sequence
                .Select(number => number / (number - 5))
                .OnErrorResumeNext(Observable.Return(int.MinValue))
                .Finally(() => done.Set())
                .Subscribe(new ConsoleObserver());
            done.WaitOne();

        }

        [Fact]
        public void ShouldIterateAllSources()
        {
            var sequences = new IObservable<int>[]
                {
                    Observable.Range(10, 10)
                        .Select(number =>  number / (number % 7)), //start with 3
                    Observable.Range(22, 10)
                        .Select(number => number / (number % 7)), // start with 22
                    Observable.Range(33, 10)
                        .Select(number => number / (number % 7)), // start with 6
                    Observable.Range(44, 10)
                      .Select(number => number / (number % 7)), // start with 22
                    Observable.Range(55, 10)
                        .Select(number => number / (number % 7)), // start with 9
                };
            sequences.OnErrorResumeNext().Subscribe(new ConsoleObserver());
            
            //sequences.Catch().
            //          Subscribe(new ConsoleObserver());

        }

        [Fact]
        public void ShouldCatchException()
        {
            var sequence = Observable.Range(1,
                10);
            sequence.Select(number =>
            {
                if (number == 7)
                    throw new InvalidOperationException();
                return number;
            }).
                     //Catch((InvalidOperationException ex) =>
                     //{
                     //    Console.WriteLine("invalid op");
                     //    return Observable.Empty<int>();
                     //}).
                     Catch((Exception ex) =>
                     {
                         Console.WriteLine(ex.Message);
                         return Observable.Range(20, 4);
                     })
                     .Subscribe(new ConsoleObserver());
        }
    }
}