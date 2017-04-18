using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
        private ObservableCollection<NuGetPackage> _package;
        private string _searchParam;

        public PackageListViewModel(ICredentialProvider credentialProvider)
        {
            _credentialProvider = credentialProvider;
            SearchParam = "Search";
            Packages = new ObservableCollection<NuGetPackage>();
            RefreshCommand = new DelegateCommand(OnClickRefresh);
        }

        public ObservableCollection<NuGetPackage> Packages
        {
            get => _package;
            set => SetProperty(ref _package, value);
        }

        public DelegateCommand RefreshCommand { get; set; }

        public string SearchParam
        {
            get => _searchParam;
            set => SetProperty(ref _searchParam, value);
        }

        private void OnClickRefresh()
        {
            var creds = _credentialProvider.GetCredentials();
            Task.Run(async () =>
            {
                Packages = await SearchPackages(creds, SearchParam);

            });
        }

        private async Task<ObservableCollection<NuGetPackage>> SearchPackages(Credentials creds, string searchParam)
        {
            var manager = new PackageManager(new NuGetConnection(creds.UserName, creds.Password, creds.PackageUri));

            List<NuGetPackage> packages = new List<NuGetPackage>();
            try
            {
                var searchMetadatasPackages = await manager.SearchPackagesAsync(searchParam);
                foreach (var searchMetadata in searchMetadatasPackages)
                {
                    var package = new NuGetPackage();
                    var versions = await searchMetadata.GetVersionsAsync();
                    package.Title = searchMetadata.Title;
                    package.Description = searchMetadata.Description;
                    package.Versions = versions.Select(v => new NuGetPackageVersion()
                    {
                        Version = v.Version.Version.ToString(),
                        DownloadCount = v.Version.Version.ToString()
                    });
                    packages.Add(package);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
           
            return new ObservableCollection<NuGetPackage>(packages);
        }
        
    }

   
}