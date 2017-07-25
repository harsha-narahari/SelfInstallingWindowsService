using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfInstallingService
{
    public class MyServiceLogic : IServiceLogic
    {
        public string ServiceDescription
        {
            get;
            set;
        }

        public string ServiceDisplayName
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }        

        public void Start(IEnumerable<string> args)
        {
            Debug.WriteLine("Start(): {0}", string.Join(", ", args));
            Logger.LogMessage("Starting service");
        }

        public void Stop()
        {
            // Do nothing.
        }
    }
}
