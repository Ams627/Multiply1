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

        public string QuestionString
        {
            get { return (string)GetValue(QuestionStringProperty); }
            set { SetValue(QuestionStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QuestionString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QuestionStringProperty =
            DependencyProperty.Register("QuestionString", typeof(string), typeof(VoiceOver), new PropertyMetadata("", new PropertyChangedCallback(OnQuestionChanged)));



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
            if (Application.Current is App && !string.IsNullOrWhiteSpace(QuestionString))
            {
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child, 0, ukculture);
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;     // -10...10
                synthesizer.SpeakAsync(QuestionString);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
