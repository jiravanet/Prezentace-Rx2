using System;
using System.Linq;
using System.Reactive.Linq;
using Xunit;

namespace Rx
{
    public class SkipTests
    {
        [Fact]
        public void ShouldSkipFirstThree()
        {
            var sequence = Observable.Range(1,
                10);
            sequence.Where(number => number > 4).Skip(3).Subscribe(new ConsoleObserver());
        }

        [Fact]
        public void ShouldSkipLastThree()
        {
            var sequence = Observable.Range(1,
                10);
            sequence.Do(new ConsoleObserver("Do")).SkipLast(3).Subscribe(new ConsoleObserver());
        }

        [Fact]
        public void ShouldSkipWhile()
        {
            var sequence = (from number in new[] { 4, 5, 2, 6, 3, 1, 0 } select number)
                .ToObservable();
            sequence.SkipWhile((value) => value <= 5)
                .Subscribe(new ConsoleObserver());

        }

        [Fact]
        public void ShouldSkipWhileWithIndex()
        {
            var sequence = (from number in new[] { 4, 5, 2, 6, 3, 1, 0 } select number)
                .ToObservable();
            sequence.SkipWhile((value, index) => value <= 5 && (index < 2))
                .Subscribe(new ConsoleObserver());

        }

        [Fact]
        public void ShouldSkipUntil()
        {
            var firstSequence = (from number in new int[] { 4, 5, 3, 6, 2, 1, 0 }
                                 select number).ToObservable().
                                                Do(new ConsoleObserver("FDo "));

            var secondSequence = (from number in Enumerable.Range(0,
                10)
                                  select number).ToObservable().
                                                 Do(new ConsoleObserver("Do ")).
                                                 Skip(4).
                                                 Do(new ConsoleObserver("SDo ")).
                                                 Finally(() => Console.WriteLine("second finally"));

            var sequence = firstSequence.SkipUntil(secondSequence)
                .Subscribe(new ConsoleObserver());
        }
    }
}