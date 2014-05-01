using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using vJoyInterfaceWrap;

namespace VJoyTCPService
{
    public class JoyFactory
    {
        private readonly ICollection<JoyControl> _joystics;

        public JoyFactory()
        {
            _joystics = new Collection<JoyControl>
            {
                new JoyControl(5),
                new JoyControl(6),
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
            foreach (var joy in _joystics.Where(joy => joy.Capabilities.Token == capabilities.Token))
            {
                return joy.Disconnect();
            }
            return false;
        }

        public void PostCurrentJoystickState(vJoy.JoystickState joyState, Guid token)
        {
            foreach (var joy in _joystics.Where(joy => joy.Capabilities.Token == token))
            {
                joy.PostCurrentState(joyState);
                return;
            }
        }
    }
}
