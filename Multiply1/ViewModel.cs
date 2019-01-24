using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using MvvmFoundation.Wpf;
using System.Speech.Synthesis;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Multiply1
{
    public class BinarySum
    {
        private readonly Random _random = new Random();
        public enum Operators
        {
            Plus, Minus, Multiply, Divide
        }
        public int First { get; set; }
        public int Second { get; set; }
        public Operators Operator { get; set; } = Operators.Plus;

        public int Result()
        {
            switch (Operator)
            {
                case Operators.Plus:
                    return First + Second;
                case Operators.Minus:
                    return First - Second;
                case Operators.Multiply:
                    return First * Second;
                case Operators.Divide:
                    return First / Second;
                default:
                    throw new Exception("Invalid Operator");
            }
        }

        public void Randomise()
        {
            First = _random.Next(1, 10);
            Second = _random.Next(1, 10);
        }
    }
    public class ViewModel : DependencyObject, INotifyPropertyChanged
    {
        private readonly CultureInfo ukculture = new CultureInfo("en-gb");
        public RelayCommand AnswerCommand { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private void OnAnswer()
        {
            if (!string.IsNullOrWhiteSpace(Answer))
            {
                var numericalAnswer = Convert.ToInt32(Answer);
                if (numericalAnswer == Multiplicand * Multiplier)
                {
                    SpeakCorrect();
                }
                else
                {
                    SpeakWrong();
                }
                System.Diagnostics.Debug.WriteLine($"Answered!");
                Answer = "";
                var rnd = new Random();
                Multiplier = rnd.Next(1, 5);
                Multiplicand = rnd.Next(1, 5);

                //MultiplierProperty.in

                System.Diagnostics.Debug.WriteLine($"new sum is {Multiplicand} x {Multiplier}");
            }
        }

        private void SpeakWrong()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child, 0, ukculture);
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10

            var stringToSpeak = $"Wrong!";
            synthesizer.Speak(stringToSpeak);
        }

        private void SpeakCorrect()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child, 0, ukculture);
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10

            var stringToSpeak = $"Correct!";
            synthesizer.Speak(stringToSpeak);
        }



        public ViewModel()
        {
            AnswerCommand = new RelayCommand(OnAnswer);
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(5000)
            };
            timer.Tick += (s, e) =>
            {
                Sum.Randomise();
                System.Diagnostics.Debug.WriteLine($"Multiplicand is {Multiplicand}");
            };
            timer.Start();
        }



        public BinarySum Sum
        {
            get { return (BinarySum)GetValue(SumProperty); }
            set { SetValue(SumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SumProperty =
            DependencyProperty.Register("Sum", typeof(BinarySum), typeof(ViewModel), new PropertyMetadata(new BinarySum { First = 2, Second = 3 }));



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


        public string Answer
        {
            get { return (string)GetValue(AnswerProperty); }
            set { SetValue(AnswerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Answer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnswerProperty =
            DependencyProperty.Register("Answer", typeof(string), typeof(ViewModel), new PropertyMetadata(""));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
