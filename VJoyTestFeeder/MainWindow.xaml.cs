using vJoyInterfaceWrap;
using VJoyTestFeeder.VJoyServiceReference;

namespace VJoyTestFeeder
{
    public partial class MainWindow
    {
        private ServiceClient _client;
        public vJoy.JoystickState JoystickState { get; set; }
        public JoyCapabilities JoyCapabilities { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _client = new ServiceClient();
            JoyCapabilities = _client.ConnectJoystick();

            DataContext = this;
        }
    }
}
