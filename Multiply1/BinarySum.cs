using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Multiply1
{
    public class BinarySum : INotifyPropertyChanged
    {
        private readonly Random _random = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

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
            NotifyPropertyChanged("First");
            NotifyPropertyChanged("Second");
            NotifyPropertyChanged("SpokenSum");
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string SpokenSum
        {
            get
            {
                return $"{First} times {Second}";
            }
        }

    }
}
