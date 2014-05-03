using System;
using vJoyInterfaceWrap;
using VJoyTCPService.JoysBackend;

namespace VJoyTCPService
{
    public class RemoteControllerService : IService
    {
        private static readonly JoyFactory _factory = new JoyFactory();

        public JoyCapabilities ConnectJoystick()
        {
            return _factory.TryConnect();
        }

        public bool DisconnectJoystick(JoyCapabilities capabilities)
        {
            return _factory.Disconnect(capabilities);
        }

        public void PostCurrentJoystickState(vJoy.JoystickState joyState, Guid token)
        {
            _factory.PostCurrentJoystickState(joyState, token);
        }
    }
}
