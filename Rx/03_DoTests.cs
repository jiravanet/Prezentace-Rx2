using System.Reactive.Linq;
using Xunit;

namespace Rx
{
    public class DoTests
    {
        [Fact]
        public void ShouldWriteEachNumber()
        {
            var observable = Observable.Range(1,
                10);

            var sequence = observable.Do(new ConsoleObserver("Do "));
            sequence.Subscribe(new ConsoleObserver());

        }

        [Fact]
        public void ShouldWriteEvenNumber()
        {
            var observable = Observable.Range(1,
                10);

            var sequence = observable.Do(new ConsoleObserver("Do "));
            sequence.Where(i => i % 2 == 0).
                     Subscribe(new ConsoleObserver());

        }
    }
}