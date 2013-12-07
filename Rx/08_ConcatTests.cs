using System;
using System.Linq;
using System.Reactive.Linq;
using Xunit;

namespace Rx
{
    public class ConcatTests
    {
        [Fact]
        public void ShouldConcatSimple()
        {
            const int start = 1;
            const int length = 10;

            var seq1 = (from number in Enumerable.Range(start, length) select number)
                .ToObservable();
            var seq2 = (from number in Enumerable.Range(start + length + 2, length) select number)
                .ToObservable();

            seq1.Concat(seq2).Subscribe(new ConsoleObserver());
        } 
    }
}