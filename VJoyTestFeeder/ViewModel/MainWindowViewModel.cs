using GalaSoft.MvvmLight;
using vJoyInterfaceWrap;
using VJoyTestFeeder.VJoyServiceReference;

namespace VJoyTestFeeder.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ServiceClient _client;
        private vJoy.JoystickState _joystickState;

        private JoyCapabilities _joyCapabilities;
        public JoyCapabilities JoyCapabilities
        {
            get { return _joyCapabilities; }
            set
            {
                if (value != _joyCapabilities)
                {
                    _joyCapabilities = value;
                    RaisePropertyChanged("JoyCapabilities");
                }
            }
        }
        public JoyVM JoyVM { get; set; }
        public MainWindowViewModel()
        {
            _client = new ServiceClient();
            _joystickState = new vJoy.JoystickState();

            /* Before it we should check if service is available */
            if ((JoyCapabilities = _client.ConnectJoystick()).JoyId != 0)
            {
                JoyVM = new JoyVM();
                JoyVM.PropertyChanged += JoyVM_PropertyChanged;
            }
        }

        void JoyVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _joystickState.AxisX = JoyVM.AxisX;
            _joystickState.AxisY = JoyVM.AxisY;
            _client.PostCurrentJoystickState(_joystickState, JoyCapabilities.Token);
        }

        public void OnClosing()
        {
            if (JoyCapabilities.JoyId != 0)
            {
                _client.DisconnectJoystick(JoyCapabilities);
            }
        }
    }
}
