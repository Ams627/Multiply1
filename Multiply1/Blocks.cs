using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Multiply1
{
    public class Blocks : Control
    {
        private Canvas _canvas = null;
        static Blocks()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Blocks), new FrameworkPropertyMetadata(typeof(Blocks)));
        }
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(Blocks), new PropertyMetadata(2, new PropertyChangedCallback(OnMultPropertyChanged)));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(Blocks), new PropertyMetadata(2, new PropertyChangedCallback(OnMultPropertyChanged)));

        private static void OnMultPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"BCHANGED!");
            if (d is Blocks blocksControl)
            {
                blocksControl.DrawControl();
            }
        }

        public int SquareSize
        {
            get { return (int)GetValue(SquareSizeProperty); }
            set { SetValue(SquareSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SquareSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SquareSizeProperty =
            DependencyProperty.Register("SquareSize", typeof(int), typeof(Blocks), new PropertyMetadata(100));

        public int Gap
        {
            get { return (int)GetValue(GapProperty); }
            set { SetValue(GapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Gap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GapProperty =
            DependencyProperty.Register("Gap", typeof(int), typeof(Blocks), new PropertyMetadata(10));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var canvasPart = Template.FindName("Part_Canvas", this);
            if (canvasPart is Canvas canvas)
            {
                _canvas = canvas;
                DrawControl();
            }
        }

        private void DrawControl()
        {
            int left = 0, top = 0;
            for (var i = 0; i < Columns; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    var rectangle = new Rectangle
                    {
                        Height = SquareSize,
                        Width = SquareSize,
                        Fill = new SolidColorBrush(Color.FromRgb(0x88, 0x22, (byte)(0x88 + 30 * i))),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        // Margin = new Thickness { Left = i * SquareSize + Gap, Top = j * SquareSize + Gap, Right = 0, Bottom = 0 },
                    };
                    _canvas.Children.Add(rectangle);
                    left = i * (SquareSize + Gap);
                    top = j * (SquareSize + Gap);
                    Canvas.SetLeft(rectangle, left);
                    Canvas.SetTop(rectangle, top);
                }

                // the canvas width and height must be set manually - this is the nature of the Canvas class:
                _canvas.Height = top + SquareSize;
                _canvas.Width = left + SquareSize;
            }
        }
    }
}
