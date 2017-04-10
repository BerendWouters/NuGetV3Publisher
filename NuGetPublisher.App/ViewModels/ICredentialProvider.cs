using NuGetPublisher.App.Models;

namespace NuGetPublisher.App.ViewModels
{
    public interface ICredentialProvider
    {
        Credentials GetCredentials();
    }
}