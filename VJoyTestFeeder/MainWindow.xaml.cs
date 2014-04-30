using VJoyTestFeeder.VJoyServiceReference;

namespace VJoyTestFeeder
{
    public partial class MainWindow
    {
        private ServiceClient _client;
        public int JoyId { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _client = new ServiceClient();
            JoyId = _client.ConnectJoystick();

            DataContext = this;
        }


    }
}
