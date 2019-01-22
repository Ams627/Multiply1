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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Multiply1"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Multiply1;assembly=Multiply1"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Blocks/>
    ///
    /// </summary>
    public class Blocks : Control
    {
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
            DependencyProperty.Register("Rows", typeof(int), typeof(Blocks), new PropertyMetadata(2));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(Blocks), new PropertyMetadata(2));



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
            var r = new Rectangle
            {
                Height = 100,
                Width = 100,
                Fill = Brushes.Green,
                Stroke = Brushes.Red
            };

            var canvasPart = Template.FindName("Part_Canvas", this);
            if (canvasPart is Canvas canvas)
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
                        System.Diagnostics.Debug.WriteLine($"Margin: {rectangle.Margin.Left}, {rectangle.Margin.Top}, {rectangle.Margin.Right}, {rectangle.Margin.Bottom}");
                        canvas.Children.Add(rectangle);
                        left = i * (SquareSize + Gap);
                        top = j * (SquareSize + Gap);
                        Canvas.SetLeft(rectangle, left);
                        Canvas.SetTop(rectangle, top);
                        System.Diagnostics.Debug.WriteLine($"Actual Height {canvas.ActualHeight} - actual width {canvas.ActualWidth}");
                    }
                    canvas.Height = top + SquareSize;
                    canvas.Width = left + SquareSize;
                }
            }
        }
    }
}
