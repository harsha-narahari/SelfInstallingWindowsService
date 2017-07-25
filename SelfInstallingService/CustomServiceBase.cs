using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SelfInstallingService
{
    [System.ComponentModel.DesignerCategory("Code")] // Prevent double-clicking the file to open designer mode.
    public class CustomServiceBase : ServiceBase
    {
        private readonly IServiceLogic _serviceLogic;

        public CustomServiceBase(IServiceLogic serviceLogic)
        {
            ServiceName = serviceLogic.ServiceName;
            _serviceLogic = serviceLogic;
        }

        protected override void OnStart(string[] args)
        {
            _serviceLogic.Start(args);
        }

        protected override void OnStop()
        {
            _serviceLogic.Stop();
        }
    }
}
