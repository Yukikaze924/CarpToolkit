using Avalonia.Controls;
using CarpToolkit.Helpers;
using CarpToolkit.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using Serilog.Core;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarpToolkit.ViewModels
{
    partial class AccountViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private string _nickname;

        [ObservableProperty]
        private string _account;

        public AccountViewModel()
        {
            var cache = CacheHelper.Load();
            IsLoggedIn = cache.isLoggedIn ? cache.isLoggedIn : false;
            Account = cache.account ?? "Become our user to learn more about features";
            Nickname = cache.nickname ?? "Not logged in";
        }


        [RelayCommand]
        private async Task LaunchLoginDialogAsync()
        {
            var dialog = new ContentDialog()
            {
                Title = "Login",
                PrimaryButtonText = "Next",
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Primary,
            };

            // In our case the Content is a UserControl, but can be anything.
            dialog.Content = new LoginPageViewModel();

            var result = await dialog.ShowAsync();

            switch (result)
            {
                case ContentDialogResult.Primary:

                    LoginPageViewModel vm = (LoginPageViewModel)dialog.Content;

                    try
                    {
                        UserModel? user = await HttpHelper.LoginPostAsync("http://hakuryuu25500.top:8000/login/", vm.Account, vm.Password);
                        if (user!=null && user.isUserValid)
                        {
                            var cache = CacheHelper.Load();
                            cache.isLoggedIn = true;
                            cache.account = user.account;
                            cache.nickname = user.nickname;
                            CacheHelper.Save();

                            CurrentPageChanged?.Invoke(null, new AccountViewModel());
                            ShowResultDialog($"Welcome!", $"You're currently logged in as {user.nickname}{Environment.NewLine}{Environment.NewLine}Account: {user.account}");
                            return;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch(Exception ex) when (ex is NullReferenceException)
                    {
                        SerilogService.logger.Error("Login Post Request returns a null");
                    }
                    catch(Exception ex)
                    {
                        SerilogService.logger.Error($"Unknown Http Post Error occurred{Environment.NewLine}{ex.Message}");
                    }

                    ShowResultDialog("Something went wrong...", "Probably our server crashed again (`T_T)");

                    break;
            }
        }

        [RelayCommand]
        private async Task GetStartedAsync()
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://formally-wanted-yak.ngrok-free.app/register")
                { UseShellExecute = true, Verb = "open" });
            }
            catch
            {
                await DialogHelper.ShowUnableToOpenLinkDialog(new Uri("https://formally-wanted-yak.ngrok-free.app/register"));
            }
        }

        [RelayCommand]
        private void SignOut()
        {
            var cache = CacheHelper.Load();
            cache.isLoggedIn = false;
            cache.account = null;
            cache.nickname = null;
            CacheHelper.Save();
            CurrentPageChanged?.Invoke(null, new AccountViewModel());
        }

        private async void ShowResultDialog(string header, string subheader)
        {
            var dialog = new ContentDialog()
            {
                Title = header,
                CloseButtonText = "Close",
            };

            dialog.Content = new TextBlock()
            {
                Text = subheader
            };

            var result = await dialog.ShowAsync();
        }
    }
}
