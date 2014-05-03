using vJoyInterfaceWrap;
using VJoyTestFeeder.VJoyServiceReference;

namespace VJoyTestFeeder
{
    public partial class MainWindow
    {
        private ServiceClient _client;
        private static vJoy.JoystickState _joystickState { get; set; }
        public JoyCapabilities JoyCapabilities { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _client = new ServiceClient();
            _joystickState = new vJoy.JoystickState();
            JoyCapabilities = _client.ConnectJoystick();

            DataContext = this;
        }
    }
}
