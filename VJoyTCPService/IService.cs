﻿using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using vJoyInterfaceWrap;

namespace VJoyTCPService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        JoyCapabilities ConnectJoystick();

        [OperationContract]
        bool DisconnectJoystick(JoyCapabilities capabilities);

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
            Token = new Guid();
        }

        // JoyId = 0 if there was an error
        [DataMember] 
        public int JoyId { get; set; }

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
        public int AxisXMax { get; set; }

        [DataMember]
        public int AxisYMax { get; set; }

        [DataMember]
        public Guid Token { get; set; }

        [DataMember]
        public DateTime DateTime { get; set; }
    }
}
