using System;
using vJoyInterfaceWrap;

namespace VJoyTCPService
{
    public class RemoteControllerService : IService
    {
        public int ConnectJoystick()
        {
            //TODO: Everything
            return 1;
        }

        public void PostCurrentJoystickState(int joyId, vJoy.JoystickState joyState)
        {
            //TODO: Everything
        }
    }
}
