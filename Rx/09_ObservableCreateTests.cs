using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xunit;

namespace Rx
{
    public class ObservableCreateTests
    {
        [Fact]
        public void ShouldCreateObservable()
        {
            var sequence = ObservableCreate.Create();
            sequence.Subscribe(new ConsoleObserver());
        }
    }

    public static class ObservableCreate
    {
        public static IObservable<int> Create()
        {
            return Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);
                observer.OnCompleted();
                return (() => { });
                //return Disposable.Empty;
            });
        } 
    }
}