using Avalonia.Styling;
using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarpToolkit.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<string> _appThemes = new()
        {
            "Default", "Light", "Dark"
        };

        [ObservableProperty]
        private string? _currentAppTheme = SettingsHelper.Settings.AppTheme;

        partial void OnCurrentAppThemeChanged(string? value)
        {
            SettingsHelper.ChangeThemeVariantByString(value);
            //if (value != null)
            //{
            //    switch (value)
            //    {
            //        case "Default":
            //            App.Current!.RequestedThemeVariant = ThemeVariant.Default;
            //            SettingsHelper.Settings.AppTheme = value;
            //            SettingsHelper.Save();
            //            break;

            //        case "Light":
            //            App.Current!.RequestedThemeVariant = ThemeVariant.Light;
            //            SettingsHelper.Settings.AppTheme = value;
            //            SettingsHelper.Save();
            //            break;

            //        case "Dark":
            //            App.Current!.RequestedThemeVariant = ThemeVariant.Dark;
            //            SettingsHelper.Settings.AppTheme = value;
            //            SettingsHelper.Save();
            //            break;
            //    }
            //}   
        }

        [ObservableProperty]
        private bool _isPaneOpen = SettingsHelper.Load().IsPaneOpen;

        partial void OnIsPaneOpenChanged(bool value)
        {
            SettingsHelper.Settings.IsPaneOpen = value;
            SettingsHelper.Save();
        }

        [RelayCommand]
        private async Task LaunchRepoLinkItemClickedAsync()
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://formally-wanted-yak.ngrok-free.app/contact")
                { UseShellExecute = true, Verb = "open" });
            }
            catch
            {
                await DialogHelper.ShowUnableToOpenLinkDialog(new System.Uri("https://formally-wanted-yak.ngrok-free.app/contact"));
            }
        }
    }
}
