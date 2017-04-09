using System;
using System.Collections.Generic;
using System.Configuration;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Core.v2;

namespace NuGetPublisher.Management
{
    public class NuGetConnection
    {
        private static ILogger _logger;
        private readonly string _pat;
        private readonly string _username;

        public NuGetConnection(string username, string pat)
        {
            _logger = new CustomLogger();
            _username = username;
            _pat = pat;
            SetupConnection();
        }

        public SourceRepository SourceRepository { get; private set; }

        public ILogger Logger => _logger;

        private void SetupConnection()
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3()); // Add v3 API support
            providers.AddRange(Repository.Provider.GetCoreV2()); // Add v2 API support
            var reader = new AppSettingsReader();
            var nugetSource = reader.GetValue("nugetSource", typeof(string));
            var packageSource = new PackageSource(nugetSource.ToString())
            {
                Credentials = PackageSourceCredential.FromUserInput("sourceID", _username, _pat, true)
            };
            SourceRepository = new SourceRepository(packageSource, providers);
        }
    }
}