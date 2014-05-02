using System;
using System.Runtime.CompilerServices;
using vJoyInterfaceWrap;
using VJoyTCPService.Infrastructure;

namespace VJoyTCPService.JoysBackend
{
    /// <summary>
    /// Unit controlling virtual joystick with ID specified in constructor
    /// </summary>
    public class JoyControl
    {
        private readonly uint _joyId;
        private readonly vJoy _joystick;

        /// <summary>
        /// Determines if current joystick is in use
        /// </summary>
        public bool IsBusy { get; private set; }
        public JoyCapabilities Capabilities { get; private set; }

        /// <summary>
        /// Creates a new instance of joystick
        /// </summary>
        /// <param name="joyId">Id of simulated joystick between (1,16). It have to exists in system before the invokation.</param>
        public JoyControl(uint joyId)
        {
            if(!joyId.Between(1,16, true))
                throw new Exception(String.Format("Illegal device ID {0}", joyId));
            IsBusy = false;
            _joyId = joyId;
            _joystick = new vJoy();
        }

        public JoyCapabilities TryConnect()
        {
            if (IsBusy)
                throw new Exception("Cannot connect to a joystick already connected.");

            /* Creating a new instance every time when we start a new connection
             * because we have to refresh the access token to preserve the uniqueness
             * among all connection attempts */
            Capabilities = new JoyCapabilities();

            /* Checking if the vJoy driver is enabled */
            if (!_joystick.vJoyEnabled())
            {
                Capabilities.ErrorInfo = "vJoy driver not enabled.";
                return Capabilities;
            }

            /* For information purposes only */
            Capabilities.JoyInfo = String.Format("Vendor: {0}, Product :{1}, Version Number:{2}.",
                _joystick.GetvJoyManufacturerString(), _joystick.GetvJoyProductString(),
                _joystick.GetvJoySerialNumberString());

            /* Checking eventual problems with occupied or not installed joystick */
            var status = _joystick.GetVJDStatus(_joyId);

            switch (status)
            {
                case VjdStat.VJD_STAT_OWN:
                case VjdStat.VJD_STAT_FREE:
                    break;
                case VjdStat.VJD_STAT_BUSY:
                    Capabilities.ErrorInfo = String.Format("vJoy Device {0} is already owned by another feeder. Cannot continue.", _joyId);
                    return Capabilities;
                case VjdStat.VJD_STAT_MISS:
                    Capabilities.ErrorInfo = String.Format("vJoy Device {0} is not installed or disabled. Cannot continue.", _joyId);
                    return Capabilities;
                default:
                    Capabilities.ErrorInfo = String.Format("vJoy Device {0} general error. Cannot continue.", _joyId);
                    return Capabilities;
            }

            /* ##### Remark #####
             * Generally a situation that in this moment the joystick is in state VJD_STAT_OWN shouldn't be possible
             * and we can consider to throw an exception when the situation occurs. */

            /* Engaging joystick with specified id with the current instance of JoyControl */
            if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!_joystick.AcquireVJD(_joyId))))
            {
                Capabilities.ErrorInfo = String.Format("Failed to acquire vJoy device number {0}.", _joyId);
                return Capabilities;
            }

            IsBusy = true;
            _joystick.ResetVJD(_joyId);

            Capabilities.JoyId = _joyId;

            Capabilities.AxisXExists = _joystick.GetVJDAxisExist(_joyId, HID_USAGES.HID_USAGE_X);
            Capabilities.AxisXExists = _joystick.GetVJDAxisExist(_joyId, HID_USAGES.HID_USAGE_Y);
            Capabilities.ButtonsCount = _joystick.GetVJDButtonNumber(_joyId);

            long max = 0;

            _joystick.GetVJDAxisMax(_joyId, HID_USAGES.HID_USAGE_X, ref max);
            Capabilities.AxisXMax = max;

            _joystick.GetVJDAxisMax(_joyId, HID_USAGES.HID_USAGE_X, ref max);
            Capabilities.AxisYMax = max;

            return Capabilities;
        }

        /// <summary>
        /// Disconnect the current instance of joystick from currently bound virtual device.
        /// </summary>
        /// <returns>The success of the disconnect operation</returns>
        public bool Disconnect()
        {
            if (!IsBusy)
                throw new Exception("You cannot disconnect a not connected joystick.");

            _joystick.RelinquishVJD(_joyId);
            IsBusy = false;

            return true;
        }

        /// <summary>
        /// Updates a state of joystick (values of buttons, axes, etc.) using a JoystickState struct
        /// </summary>
        /// <param name="joyState">Struct storing updated values of joystick state</param>
        public void PostCurrentState(vJoy.JoystickState joyState)
        {
            if(!IsBusy) 
                throw new Exception("You cannot change a state of a joystick which isn't connected.");

            if (!_joystick.UpdateVJD(_joyId, ref joyState))
            {
                Disconnect();
            }
        }
    }
}
