using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using vJoyInterfaceWrap;

namespace VJoyTCPService.JoysBackend
{
    /// <summary>
    /// Class-container of available joysticks and management for them
    /// </summary>
    internal class JoyFactory
    {
        private readonly ICollection<JoyControl> _joystics;

        /// <summary>
        /// Creates a new instance of JoyFactory and adds joysticks available by default
        /// </summary>
        public JoyFactory()
        {
            _joystics = new Collection<JoyControl>
            {
                new JoyControl(5), // 5 is the device ID which has to be between (1,15) and must exists in system
                new JoyControl(6), // 6 is the device ID which has to be between (1,15) and must exists in system
            };
        }

        /// <summary>
        /// Checks if are there any free joysticks and connects then.
        /// </summary>
        /// <returns>Struct containing information about engaged joystick or (in case of error)
        /// information describing eventual problems with request</returns>
        public JoyCapabilities TryConnect()
        {
            foreach (var joy in _joystics.Where(joy => !joy.IsBusy))
            {
                return joy.TryConnect();
            }

            return new JoyCapabilities { ErrorInfo = "All joysticks are currently busy" };
        }

        /// <summary>
        /// Finds and releases the joystick identified and authorized by JoyCapabilities struct.
        /// </summary>
        /// <param name="capabilities">Struct containing a token which is checked for safety
        /// - to make sure that the current owner of joystick wants to release the joystick</param>
        /// <returns>Success of disconnect operation</returns>
        public bool Disconnect(JoyCapabilities capabilities)
        {
            foreach (var joy in _joystics.Where(joy => joy.IsBusy && joy.Capabilities.Token == capabilities.Token))
            {
                return joy.Disconnect();
            }
            return false;
        }

        /// <summary>
        /// Finds and updates a state of joystick identified and authorized by token.
        /// </summary>
        /// <param name="joyState">Struct with updated values of joystick axes, buttons, etc.</param>
        /// <param name="token">Authorizes the request</param>
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
