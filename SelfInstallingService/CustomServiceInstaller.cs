using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SelfInstallingService
{
    [System.ComponentModel.DesignerCategory("Code")] // Prevent double-clicking the file to open designer mode.
    public class CustomServiceInstaller : Installer
    {
        public static void Install(IServiceLogic service)
        {
            var installer = GetInstaller(service);
            installer.Install(new Hashtable());
        }

        public static void Uninstall(IServiceLogic service)
        {
            var installer = GetInstaller(service);
            installer.Uninstall(null);
        }

        public CustomServiceInstaller(IServiceLogic service)
        {
            ServiceProcessInstaller serviceProcessInstaller =
                       new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            // Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            // Service Information
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            // Info from ServiceLogic
            serviceInstaller.ServiceName = service.ServiceName;
            serviceInstaller.DisplayName = service.ServiceDisplayName;
            serviceInstaller.Description = service.ServiceDescription;

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }

        private static TransactedInstaller GetInstaller(IServiceLogic service)
        {
            var serviceAssembly = service.GetType().Assembly;

            string[] commandLine = new[] { String.Format("/assemblypath={0}", serviceAssembly.Location) };
            string logFile = string.Format("{0}.installlog", serviceAssembly.FullName);

            InstallContext installContext = new InstallContext(logFile, commandLine);
            TransactedInstaller transactedInstaller = new TransactedInstaller();

            transactedInstaller.Installers.Add(new CustomServiceInstaller(service));
            transactedInstaller.Context = installContext;

            return transactedInstaller;
        }
    }
}
