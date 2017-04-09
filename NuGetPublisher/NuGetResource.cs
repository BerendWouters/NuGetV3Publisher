using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Core.v2;
using NuGetPublisher;

public class NuGetResource
{
    private static ILogger _logger;
    private SourceRepository _sourceRepository;

    public NuGetResource()
    {
        _logger = new CustomLogger();
    }

    public void SetupConnection(string username, string pat)
    {
        var providers = new List<Lazy<INuGetResourceProvider>>();
        providers.AddRange(Repository.Provider.GetCoreV3()); // Add v3 API support
        providers.AddRange(Repository.Provider.GetCoreV2()); // Add v2 API support
        var reader = new AppSettingsReader();
        var nugetSource = reader.GetValue("nugetSource", typeof(string));
        var packageSource = new PackageSource(nugetSource.ToString()) {Credentials = PackageSourceCredential.FromUserInput("sourceID", username, pat, true)};
        _sourceRepository = new SourceRepository(packageSource, providers);
    }

    public async Task<IEnumerable<IPackageSearchMetadata>> SearchPackages(string seachString)
    {
        var packageMetadataResource = await _sourceRepository.GetResourceAsync<PackageSearchResource>();
        var searchMetadata = await packageMetadataResource.SearchAsync(seachString,
            new SearchFilter(true), 0, 10, _logger, CancellationToken.None);
        return searchMetadata;
    }

    public async Task UploadPackage()
    {
        var packageUpload = _sourceRepository.GetResource<PackageUpdateResource>();
        var packagePath =
            "pathtopackage.nupkg";

        await packageUpload.Push(packagePath, string.Empty, 60, false, s => s, s => s, _logger);
    }
}