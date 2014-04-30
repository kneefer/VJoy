using System.ServiceProcess;
using System.ServiceModel;
using System.Threading;

namespace EgzotechVJoyService
{
    public partial class JoyWindowsService : ServiceBase
    {
        internal static ServiceHost MyServiceHost = null;
        readonly Thread _hostThread;

        public JoyWindowsService()
        {
            InitializeComponent();
            _hostThread = new Thread(new ThreadStart(StartHosting));
        }

        protected override void OnStart(string[] args)
        {
            _hostThread.Start();
        }

        protected void StartHosting()
        {
            if (MyServiceHost != null)
            {
                MyServiceHost.Close();
            }
            MyServiceHost = new ServiceHost(typeof(VJoyTCPService.RemoteControllerService));
            MyServiceHost.Open();
        }

        protected override void OnStop()
        {
            if (MyServiceHost != null)
            {
                MyServiceHost.Close();
                MyServiceHost = null;
            }
        }
    }
}
