using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfInstallingService
{
    public interface IServiceLogic
    {
        string ServiceDescription { get; set; }
        string ServiceDisplayName { get; set; }
        string ServiceName { get; set; }

        void Start(IEnumerable<string> args);

        void Stop();
    }
}
