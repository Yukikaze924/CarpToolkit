﻿using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CarpToolkit.Helpers;
using CarpToolkit.Pages;
using CarpToolkit.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using Serilog;
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
        [ObservableProperty]
        private Bitmap _avatar;

        public AccountViewModel()
        {
            var cache = CacheHelper.Load();
            IsLoggedIn = cache.isLoggedIn ? cache.isLoggedIn : false;
            Account = cache.account ?? "Become our user to learn more about features";
            Nickname = cache.nickname ?? "Not logged in";
            if (string.IsNullOrEmpty(cache.avatar))
            {
                Avatar = ImageHelper.LoadFromResource(new Uri("avares://CarpToolkit/Assets/Icons/AccountIcon.png"));
            }
            else
            {
                Avatar = ImageHelper.LoadFromBase64(cache.avatar);
            }
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
                        UserModel? user = await HttpHelper.LoginPostAsync(vm.Account, vm.Password);
                        if (user!=null && user.isUserValid)
                        {
                            var cache = CacheHelper.Load();
                            cache.isLoggedIn = true;
                            cache.account = user.account;
                            cache.nickname = user.nickname;
                            cache.password = user.password;
                            cache.avatar = user.avatar;
                            CacheHelper.Save();

                            var logger = Ioc.Default.GetRequiredService<ILogger>();
                            logger.Information($"User {user.nickname}({user.account}) has logged in");

                            CurrentPageChanged?.Invoke(null, new AccountViewModel());
                            DialogHelper.ShowResultDialog($"Welcome!", $"You're currently logged in as {user.nickname}{Environment.NewLine}{Environment.NewLine}Account: {user.account}");
                            return;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch(Exception ex) when (ex is NullReferenceException)
                    {
                        Ioc.Default.GetRequiredService<ILogger>().Error("No response from the Host");
                    }
                    catch(Exception ex)
                    {
                        Ioc.Default.GetRequiredService<ILogger>().Error($"Unknown Http Error occurred{Environment.NewLine}{ex.Message}");
                    }

                    DialogHelper.ShowResultDialog("Something went wrong...", "Probably our server crashed again (`T_T)");

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
            var logger = Ioc.Default.GetRequiredService<ILogger>();

            logger.Information($"User {cache.nickname}({cache.account}) has signed out");

            cache.isLoggedIn = false;
            cache.account = null;
            cache.nickname = null;
            cache.password = null;
            cache.avatar = null;
            CacheHelper.Save();

            CurrentPageChanged?.Invoke(null, new AccountViewModel());
        }

        //private async void ShowResultDialog(string header, string subheader)
        //{
        //    var dialog = new ContentDialog()
        //    {
        //        Title = header,
        //        CloseButtonText = "Close",
        //    };

        //    dialog.Content = new TextBlock()
        //    {
        //        Text = subheader
        //    };

        //    await dialog.ShowAsync();
        //}
    }
}
