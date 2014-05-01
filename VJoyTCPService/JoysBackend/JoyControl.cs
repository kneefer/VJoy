using System;
using vJoyInterfaceWrap;

namespace VJoyTCPService.JoysBackend
{
    public class JoyControl
    {
        private readonly int _joyId;
        private readonly vJoy _joystick;
        public bool IsBusy { get; private set; }
        public JoyCapabilities Capabilities { get; private set; }

        public JoyControl(int joyId)
        {
            IsBusy = false;
            _joyId = joyId;
            _joystick = new vJoy();
        }

        public JoyCapabilities TryConnect()
        {
            if (IsBusy) throw new Exception("Cannot connect to the joystick already connected.");

            Capabilities = new JoyCapabilities
            {
                
            };

            return Capabilities;
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public void PostCurrentState(vJoy.JoystickState joyState)
        {
            throw new NotImplementedException();
        }
    }
}
