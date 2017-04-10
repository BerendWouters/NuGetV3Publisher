using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NuGet.Protocol.Core.Types;
using NuGetPublisher.App.Models;
using NuGetPublisher.Management;
using Prism.Commands;
using Prism.Mvvm;

namespace NuGetPublisher.App.ViewModels
{
    public class PackageListViewModel : BindableBase
    {
        private readonly ICredentialProvider _credentialProvider;
        private ObservableCollection<NugetPackage> _package;

        public PackageListViewModel(ICredentialProvider credentialProvider)
        {
            _credentialProvider = credentialProvider;
            Packages = new ObservableCollection<NugetPackage>();
            RefreshCommand = new DelegateCommand(OnClickRefresh);
        }

        public ObservableCollection<NugetPackage> Packages
        {
            get => _package;
            set => SetProperty(ref _package, value);
        }

        public DelegateCommand RefreshCommand { get; set; }

        private void OnClickRefresh()
        {
            var creds = _credentialProvider.GetCredentials();
            Task.Run(async () =>
            {
                Packages = await SearchPackages(creds, string.Empty);

            });
        }

        private async Task<ObservableCollection<NugetPackage>> SearchPackages(Credentials creds, string searchParam)
        {
            var manager = new PackageManager(new NuGetConnection(creds.UserName, creds.Password));
            IEnumerable<IPackageSearchMetadata> searchMetadatasPackages = await manager.SearchPackagesAsync(searchParam);
            List<NugetPackage> packages = searchMetadatasPackages.Select(packageSearchMetadata => new NugetPackage()
                {
                    Title = packageSearchMetadata.Title,
                    Description = packageSearchMetadata.Description
                })
                .ToList();
            return new ObservableCollection<NugetPackage>(packages);
        }
        
    }
}