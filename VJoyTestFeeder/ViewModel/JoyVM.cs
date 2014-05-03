using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using VJoyTestFeeder.Annotations;

namespace VJoyTestFeeder.ViewModel
{
    public sealed class JoyVM : INotifyPropertyChanged
    {
        private int _axisY;
        private int _axisX;
        private bool _button1;
        private bool _button2;
        private bool _button3;
        private bool _button4;
        private bool _button5;
        private bool _button6;
        private bool _button7;
        private bool _button8;

        public int AxisX
        {
            get { return _axisX; }
            set
            {
                if (value == _axisX) return;
                _axisX = value;
                OnPropertyChanged();
            }
        }
        public int AxisY
        {
            get { return _axisY; }
            set
            {
                if (value == _axisY) return;
                _axisY = value;
                OnPropertyChanged();
            }
        }

        public bool Button1
        {
            get { return _button1; }
            set
            {
                if (value.Equals(_button1)) return;
                _button1 = value;
                OnPropertyChanged();
            }
        }
        public bool Button2
        {
            get { return _button2; }
            set
            {
                if (value.Equals(_button2)) return;
                _button2 = value;
                OnPropertyChanged();
            }
        }
        public bool Button3
        {
            get { return _button3; }
            set
            {
                if (value.Equals(_button3)) return;
                _button3 = value;
                OnPropertyChanged();
            }
        }
        public bool Button4
        {
            get { return _button4; }
            set
            {
                if (value.Equals(_button4)) return;
                _button4 = value;
                OnPropertyChanged();
            }
        }
        public bool Button5
        {
            get { return _button5; }
            set
            {
                if (value.Equals(_button5)) return;
                _button5 = value;
                OnPropertyChanged();
            }
        }
        public bool Button6
        {
            get { return _button6; }
            set
            {
                if (value.Equals(_button6)) return;
                _button6 = value;
                OnPropertyChanged();
            }
        }
        public bool Button7
        {
            get { return _button7; }
            set
            {
                if (value.Equals(_button7)) return;
                _button7 = value;
                OnPropertyChanged();
            }
        }
        public bool Button8
        {
            get { return _button8; }
            set
            {
                if (value.Equals(_button8)) return;
                _button8 = value;
                OnPropertyChanged();
            }
        }

        public uint GetButtons()
        {
            uint toReturn = 0;

            if (Button1) toReturn += (uint)(1 << 0);
            if (Button2) toReturn += (uint)(1 << 1);
            if (Button3) toReturn += (uint)(1 << 2);
            if (Button4) toReturn += (uint)(1 << 3);
            if (Button5) toReturn += (uint)(1 << 4);
            if (Button6) toReturn += (uint)(1 << 5);
            if (Button7) toReturn += (uint)(1 << 6);
            if (Button8) toReturn += (uint)(1 << 7);

            return toReturn;
        }

        /* We could create custom PropertyChangedEventEventArgs but it's only a test */

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}