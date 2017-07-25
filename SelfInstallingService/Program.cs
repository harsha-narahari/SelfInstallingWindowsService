using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SelfInstallingService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Instantiate the service logic.
            var myServiceLogic = new MyServiceLogic() { ServiceDisplayName="COS.SelfinstallingService", ServiceName= "COS.SelfinstallingService" };

            // No arguments? Run the Service and exit when service exits.
            if (!args.Any())
            {
                ServiceBase.Run(new CustomServiceBase(myServiceLogic));
                return;
            }

            // Parse arguments.
            switch (args.First().ToUpperInvariant())
            {
                case "/D":
                case "/DEBUG":
                    StartService(myServiceLogic, args);
                    break;
                case "/I":
                case "/INSTALL":
                    InstallService(myServiceLogic);
                    break;
                case "/U":
                case "/UNINSTALL":
                    UninstallService(myServiceLogic);
                    break;
                default:
                    PrintUsage();
                    break;
            }
        }

        private static void InstallService(IServiceLogic myService)
        {
            CustomServiceInstaller.Install(myService);
        }

        private static void UninstallService(IServiceLogic myService)
        {
            CustomServiceInstaller.Uninstall(myService);
        }

        /// <summary>
        /// Passes the remainder of the commandline arguments to the ServiceLogic and starts it.            
        /// </summary>
        /// <param name="serviceLogic"></param>
        /// <param name="args"></param>
        private static void StartService(IServiceLogic serviceLogic, IEnumerable<string> args)
        {
            serviceLogic.Start(args.Skip(1));
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine();
            string exeName = string.Format(" {0} ", Path.GetFileName(Assembly.GetEntryAssembly().Location));
            Console.Write(exeName + " /D[ebug]\tStart the service logic");
            Console.Write(exeName + " /I[nstall]\tInstall the executable as Windows Service");
            Console.Write(exeName + " /U[ninstall]\tUninstall the Windows Service");
        }
    }
}
