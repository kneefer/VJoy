using vJoyInterfaceWrap;

namespace VJoyTCPService
{
    public class JoyControl
    {
        public bool IsBusy { get; private set; }
        public JoyCapabilities Capabilities { get; private set; }

        public JoyControl(int joyId)
        {
            IsBusy = false;

        }

        public JoyCapabilities TryConnect()
        {
            throw new System.NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new System.NotImplementedException();
        }

        public void PostCurrentState(vJoy.JoystickState joyState)
        {
            throw new System.NotImplementedException();
        }
    }
}
