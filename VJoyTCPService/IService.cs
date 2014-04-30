using System.Runtime.Serialization;
using System.ServiceModel;
using vJoyInterfaceWrap;

namespace VJoyTCPService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int ConnectJoystick();

        [OperationContract]
        void PostCurrentJoystickState(int joyId, vJoy.JoystickState joyState);

    }
}
