using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;

namespace Rx.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //void Run_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var sequence = Observable.Range(1, 100).
        //                              Do(num => GetNumber(num));
        //    //var sequence = (from num in Enumerable.Range(1, 100)
        //    //                select GetNumber(num)).ToObservable();
        //    sequence.Subscribe(number => Result.AppendText(String.Format("{0}{1}",
        //        number,
        //        Environment.NewLine)));
        //}
        void Run_OnClick(object sender, RoutedEventArgs e)
        {
            var sequence = Observable.Range(1, 100, NewThreadScheduler.Default).
                                      Do(num => GetNumber(num));
            //var sequence = (from num in Enumerable.Range(1, 100)
            //                select GetNumber(num)).ToObservable();
            sequence.ObserveOnDispatcher().Subscribe(number => Result.AppendText(String.Format("{0}{1}",
                number,
                Environment.NewLine)));
        }

        int GetNumber(int num)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(20));
            return num;
        }
    }
}
