using NuGetPublisher.App.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace NuGetPublisher.App.ViewModels
{
    public class LoginViewModel : BindableBase, ICredentialProvider
    {
        private string _password;
        private string _userName;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public DelegateCommand ConnectCommand { get; set; }

        public Credentials GetCredentials()
        {
            return new Credentials
            {
                UserName = UserName,
                Password = Password
            };
        }
    }
}