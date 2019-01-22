using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Multiply1
{
    public class ViewModel
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
            };
            timer.Start();
        }
        public int Multiplicand { get; set; } = 4;
        public int Multiplier { get; set; } = 3;
    }
}
