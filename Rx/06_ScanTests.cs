using System;
using System.Linq;
using System.Reactive.Linq;
using Xunit;

namespace Rx
{
    public class ScanTests
    {
        [Fact]
        public void ShouldAccumulateNumbers()
        {
            var query = from number in new int[] { 3, 11, -4, 9 } select number;
            var sequence = query.ToObservable();
            var runningSum = sequence.Scan((accumulator, value) => accumulator * value);
            runningSum.Subscribe(new ConsoleObserver());

        }

        [Fact]
        public void ShouldAccumulateNumbersWithSeed()
        {
            var query = from number in new int[] { 3, 11, -4, 9 } select number;
            var sequence = query.ToObservable();
            var runningSum = sequence.Scan(10, (accumulator, value) => accumulator * value);
            runningSum.Subscribe(new ConsoleObserver());

        } 
    }
}