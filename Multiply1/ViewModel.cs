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
                if (numericalAnswer == Sum.Result())
                {
                    SpeakCorrect();
                }
                else
                {
                    SpeakWrong();
                }
                System.Diagnostics.Debug.WriteLine($"Answered!");
                Answer = "";
                Sum.Randomise();
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
            };
//            timer.Start();
        }



        public BinarySum Sum
        {
            get { return (BinarySum)GetValue(SumProperty); }
            set { SetValue(SumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SumProperty =
            DependencyProperty.Register("Sum", typeof(BinarySum), typeof(ViewModel), new PropertyMetadata(new BinarySum { First = 2, Second = 3, Operator = BinarySum.Operators.Multiply }));



        //public int Multiplicand
        //{
        //    get { return (int)GetValue(MultiplicandProperty); }
        //    set { SetValue(MultiplicandProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Multiplicand.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MultiplicandProperty =
        //    DependencyProperty.Register("Multiplicand", typeof(int), typeof(ViewModel), new PropertyMetadata(2, new PropertyChangedCallback(OnMultPropertyChanged)));

        //private static void OnMultPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //}

        //public int Multiplier
        //{
        //    get { return (int)GetValue(MultiplierProperty); }
        //    set { SetValue(MultiplierProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Multiplier.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MultiplierProperty =
        //    DependencyProperty.Register("Multiplier", typeof(int), typeof(ViewModel), new PropertyMetadata(2, new PropertyChangedCallback(OnMultPropertyChanged)));


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
