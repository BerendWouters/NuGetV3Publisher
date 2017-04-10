using System.Windows;
using Microsoft.Practices.Unity;
using NuGetPublisher.App.ViewModels;
using NuGetPublisher.Management;
using Prism.Modularity;
using Prism.Unity;

namespace NuGetPublisher.App
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            // moduleCatalog.AddModule(x);
            // moduleCatalog.AddModule(y);
            // moduleCatalog.AddModule(z);
        }

        protected override void ConfigureContainer()
        {
            // Container.RegisterInstance<>();
            base.ConfigureContainer();
            Container.RegisterType<ICredentialProvider, LoginViewModel>(new ContainerControlledLifetimeManager());
        }
    }
}