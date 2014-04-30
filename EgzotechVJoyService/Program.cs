using System.ServiceProcess;

namespace EgzotechVJoyService
{
    static class Program
    {
        static void Main()
        {
            var ServicesToRun = new ServiceBase[] 
            { 
                new JoyWindowsService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
