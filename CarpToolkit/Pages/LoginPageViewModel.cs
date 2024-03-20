using CarpToolkit.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;

namespace CarpToolkit.Pages
{
    partial class LoginPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _account;

        [ObservableProperty]
        private string _password;

        public LoginPageViewModel()
        {

        }
    }
}
