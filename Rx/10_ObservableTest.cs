﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using Xunit;

namespace Rx
{
    public class ObservableTest
    {
        [Fact]
        public async void ShouldReturnValue()
        {
            var expected = "FDA";
            var result = await Typing(expected);
            Assert.False(String.IsNullOrEmpty(result));
            Assert.True(result == expected);
        }

        [Fact]
        public void ShouldReturnValueAfterTime()
        {
            var expected = "FDA";
            var scheduler = new TestScheduler();
            var obs = Typing(expected, 5, scheduler);
            string result = null;
            obs.Subscribe(x => result = x);
            scheduler.AdvanceTo(TimeSpan.FromSeconds(3).Ticks);

            Assert.Equal(null, result);

            scheduler.AdvanceTo(TimeSpan.FromSeconds(6).Ticks);

            Assert.Equal(expected, result);

        }

        IObservable<string> Typing(string value)
        {
            return Observable.Return(value);
        }

        IObservable<string> Typing(string value, int secDelay, IScheduler scheduler = null)
        {
            return Typing(value).Delay(TimeSpan.FromSeconds(secDelay), scheduler);
        }  
    }
}