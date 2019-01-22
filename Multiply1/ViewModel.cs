using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Multiply1
{
    public class ViewModel : DependencyObject
    {
        public ViewModel()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            timer.Tick += (s, e) =>
            {
                Multiplicand++;
                System.Diagnostics.Debug.WriteLine($"Multiplicand is {Multiplicand}");
            };
            timer.Start();
        }

        public int Multiplicand
        {
            get { return (int)GetValue(MultiplicandProperty); }
            set { SetValue(MultiplicandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiplicand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MultiplicandProperty =
            DependencyProperty.Register("Multiplicand", typeof(int), typeof(ViewModel), new PropertyMetadata(2, new PropertyChangedCallback(OnMultPropertyChanged)));

        private static void OnMultPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public int Multiplier
        {
            get { return (int)GetValue(MultiplierProperty); }
            set { SetValue(MultiplierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiplier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MultiplierProperty =
            DependencyProperty.Register("Multiplier", typeof(int), typeof(ViewModel), new PropertyMetadata(2, new PropertyChangedCallback(OnMultPropertyChanged)));
    }
}
