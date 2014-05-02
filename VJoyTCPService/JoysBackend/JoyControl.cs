using System;
using System.Runtime.CompilerServices;
using vJoyInterfaceWrap;
using VJoyTCPService.Infrastructure;

namespace VJoyTCPService.JoysBackend
{
    public class JoyControl
    {
        private readonly uint _joyId;
        private readonly vJoy _joystick;
        public bool IsBusy { get; private set; }
        public JoyCapabilities Capabilities { get; private set; }

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
            if (IsBusy) throw new Exception("Cannot connect to a joystick already connected.");

            if (!_joystick.vJoyEnabled())
            {
                Capabilities.ErrorInfo = "vJoy driver not enabled.";
                return Capabilities;
            }

            Capabilities.JoyInfo = String.Format("Vendor: {0}, Product :{1}, Version Number:{2}.",
                _joystick.GetvJoyManufacturerString(), _joystick.GetvJoyProductString(),
                _joystick.GetvJoySerialNumberString());

            var status = _joystick.GetVJDStatus(_joyId);
            switch (status)
            {
                case VjdStat.VJD_STAT_OWN:
                    Capabilities.ErrorInfo = String.Format("vJoy Device {0} is already owned by this feeder.", _joyId);
                    break;
                case VjdStat.VJD_STAT_FREE:
                    Capabilities.ErrorInfo = String.Format("vJoy Device {0} is free.", _joyId);
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

            if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!_joystick.AcquireVJD(_joyId))))
            {
                Capabilities.ErrorInfo = String.Format("Failed to acquire vJoy device number {0}.", _joyId);
                return Capabilities;
            }

            IsBusy = true;

            Capabilities.AxisXExists = _joystick.GetVJDAxisExist(_joyId, HID_USAGES.HID_USAGE_X);
            Capabilities.AxisXExists = _joystick.GetVJDAxisExist(_joyId, HID_USAGES.HID_USAGE_Y);
            Capabilities.ButtonsCount = _joystick.GetVJDButtonNumber(_joyId);

            long max = 0;
            _joystick.GetVJDAxisMax(_joyId, HID_USAGES.HID_USAGE_X, ref max);
            Capabilities.AxisXMax = max;
            _joystick.GetVJDAxisMax(_joyId, HID_USAGES.HID_USAGE_X, ref max);
            Capabilities.AxisYMax = max;

            Capabilities.JoyId = _joyId;
            return Capabilities;
        }

        public bool Disconnect()
        {
            if (!IsBusy) throw new Exception("You cannot disconnect a not connected joystick.");



            IsBusy = false;
            return true;
        }

        public void PostCurrentState(vJoy.JoystickState joyState)
        {
            if(!IsBusy) throw new Exception("You cannot change a state of a joystick which isn't connected.");

            if (!_joystick.UpdateVJD(_joyId, ref joyState))
            {
                Disconnect();
            }
        }
    }
}
