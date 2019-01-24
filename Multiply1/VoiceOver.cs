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
using System.Speech.Synthesis;
using System.Globalization;
using System.ComponentModel;

namespace Multiply1
{
    public class VoiceOver : Control
    {
        private readonly CultureInfo ukculture = new CultureInfo("en-gb");
        static VoiceOver()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VoiceOver), new FrameworkPropertyMetadata(typeof(VoiceOver)));
        }

        public int Multiplicand
        {
            get { return (int)GetValue(MultiplicandProperty); }
            set { SetValue(MultiplicandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiplicand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MultiplicandProperty =
            DependencyProperty.Register("Multiplicand", typeof(int), typeof(VoiceOver), new PropertyMetadata(0, new PropertyChangedCallback(OnQuestionChanged)));


        public int Multiplier
        {
            get { return (int)GetValue(MultiplierProperty); }
            set { SetValue(MultiplierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiplier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MultiplierProperty =
            DependencyProperty.Register("Multiplier", typeof(int), typeof(VoiceOver), new PropertyMetadata(0, new PropertyChangedCallback(OnQuestionChanged)));

        private static void OnQuestionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is VoiceOver v)
            {
                v.SpeakQuestion();
            }
        }

        void SpeakQuestion()
        {
            // check if app is in design mode
            if (Application.Current is App)
            {
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child, 0, ukculture);
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;     // -10...10

                var stringToSpeak = $"{Multiplicand} times {Multiplier}";
                synthesizer.SpeakAsync(stringToSpeak);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
