using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using vJoyInterfaceWrap;

namespace VJoyTCPService.JoysBackend
{
    public class JoyFactory
    {
        private readonly ICollection<JoyControl> _joystics;

        public JoyFactory()
        {
            _joystics = new Collection<JoyControl>
            {
                new JoyControl(5), // 5 is the device ID which has to be between (1,15) and must exists in system
                new JoyControl(6), // 6 is the device ID which has to be between (1,15) and must exists in system
            };
        }

        public JoyCapabilities TryConnect()
        {
            foreach (var joy in _joystics.Where(joy => !joy.IsBusy))
            {
                return joy.TryConnect();
            }

            return new JoyCapabilities { ErrorInfo = "All joysticks are currently busy" };
        }

        public bool Disconnect(JoyCapabilities capabilities)
        {
            foreach (var joy in _joystics.Where(joy => joy.IsBusy && joy.Capabilities.Token == capabilities.Token))
            {
                return joy.Disconnect();
            }
            return false;
        }

        public void PostCurrentJoystickState(vJoy.JoystickState joyState, Guid token)
        {
            foreach (var joy in _joystics.Where(joy => joy.IsBusy && joy.Capabilities.Token == token))
            {
                joy.PostCurrentState(joyState);
                return;
            }
        }
    }
}
