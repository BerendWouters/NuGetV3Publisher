using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Protocol.Core.Types;

namespace NuGetPublisher.Management
{
    public class PackageManager
    {
        private readonly NuGetConnection _nuGetConnection;

        public PackageManager(NuGetConnection nuGetConnection)
        {
            _nuGetConnection = nuGetConnection;
        }

        public async Task<IEnumerable<IPackageSearchMetadata>> SearchPackagesAsync(string seachString)
        {
            var packageMetadataResource = await _nuGetConnection.SourceRepository
                .GetResourceAsync<PackageSearchResource>();
            var searchMetadata = await packageMetadataResource.SearchAsync(seachString,
                new SearchFilter(true), 0, 10, _nuGetConnection.Logger, CancellationToken.None);
            return searchMetadata;
        }

        public async Task UploadPackage()
        {
            var packageUpload = _nuGetConnection.SourceRepository.GetResource<PackageUpdateResource>();
            var packagePath =
                "pathtopackage.nupkg";

            await packageUpload.Push(packagePath, string.Empty, 60, false, s => s, s => s, _nuGetConnection.Logger);
        }
    }
}