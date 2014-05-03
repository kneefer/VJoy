using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using vJoyInterfaceWrap;

namespace VJoyTCPService
{
    [ServiceContract]
    public interface IService
    {
        /// <summary>
        /// Checks if are there any free joysticks and connects then.
        /// </summary>
        /// <returns>Struct containing information about engaged joystick or (in case of error)
        /// information describing eventual problems with request</returns>
        [OperationContract]
        JoyCapabilities ConnectJoystick();

        /// <summary>
        /// Finds and releases the joystick identified and authorized by JoyCapabilities struct.
        /// </summary>
        /// <param name="capabilities">Struct containing a token which is checked for safety
        /// - to make sure that the current owner of joystick wants to release the joystick</param>
        /// <returns>Success of disconnect operation</returns>
        [OperationContract]
        bool DisconnectJoystick(JoyCapabilities capabilities);

        /// <summary>
        /// Finds and updates a state of joystick identified and authorized by token.
        /// </summary>
        /// <param name="joyState">Struct with updated values of joystick axes, buttons, etc.</param>
        /// <param name="token">Authorizes the request</param>
        [OperationContract]
        void PostCurrentJoystickState(vJoy.JoystickState joyState, Guid token);

    }

    [DataContract]
    public class JoyCapabilities
    {
        public JoyCapabilities()
        {
            JoyId = 0;
            DateTime = System.DateTime.Now;
            Token = Guid.NewGuid();
        }

        [DataMember] 
        public uint JoyId { get; set; }

        [DataMember]
        public string ErrorInfo { get; set; }

        [DataMember]
        public string JoyInfo { get; set; }

        [DataMember]
        public bool AxisXExists { get; set; }

        [DataMember]
        public bool AxisYExists { get; set; }

        [DataMember]
        public int ButtonsCount { get; set; }

        [DataMember]
        public long AxisXMax { get; set; }

        [DataMember]
        public long AxisYMax { get; set; }

        [DataMember]
        public Guid Token { get; private set; }

        [DataMember]
        public DateTime DateTime { get; private set; }
    }
}
