using System.Collections.Generic;
using NuGetPublisher.App.ViewModels;

namespace NuGetPublisher.App.Models
{
    public class NuGetPackage
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public IEnumerable<NuGetPackageVersion> Versions { get; set; }
    }
    public class NuGetPackageVersion
    {
        public string DownloadCount { get; set; }
        public string Version { get; set; }
    }
}