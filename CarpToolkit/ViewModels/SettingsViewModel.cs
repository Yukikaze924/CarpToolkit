using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CarpToolkit.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarpToolkit.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isOptionsChanged = false;

        /// <summary>
        /// Bindings for variant theme.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<string> _appThemes = new()
        {
            "Default", "Light", "Dark"
        };
        [ObservableProperty]
        private string? _currentAppTheme = SettingsHelper.Load().AppTheme;
        partial void OnCurrentAppThemeChanged(string? value)
        {
            SettingsHelper.Settings.AppTheme = value;
            SettingsHelper.Save();
            SettingsHelper.ChangeThemeVariantByString(value);
            //IsOptionsChanged = true;
        }

        /// <summary>
        /// Bindings for pane's lcoation.
        /// </summary>
        [ObservableProperty]
        private List<string> _paneLocations = new List<string>()
        {
            "Left", "Top"
        };
        [ObservableProperty]
        private string _paneLocation = SettingsHelper.Load().PaneLocation;
        partial void OnPaneLocationChanged(string value)
        {
            SettingsHelper.Settings.PaneLocation = value;
            SettingsHelper.Save();
            IsOptionsChanged = true;

            if (value == "Top")
            {
                IsPaneOpenOptionEnable = false;
            }
            else
            {
                IsPaneOpenOptionEnable = true;
            }

            //PaneLocationChanged?.Invoke(null, value);
        }

        /// <summary>
        /// Bindings for pane's state of opening.
        /// </summary>
        [ObservableProperty]
        private bool _isPaneOpenOptionEnable;
        [ObservableProperty]
        private bool _isPaneOpen = SettingsHelper.Load().IsPaneOpen;
        partial void OnIsPaneOpenChanged(bool value)
        {
            SettingsHelper.Settings.IsPaneOpen = value;
            SettingsHelper.Save();
            //IsOptionsChanged=true;
        }

        public SettingsViewModel()
        {
            if (PaneLocation == "Top")
            {
                IsPaneOpenOptionEnable = false;
            }
            else
            {
                IsPaneOpenOptionEnable= true;
            }
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
                await DialogHelper.ShowUnableToOpenLinkDialog(new Uri("https://formally-wanted-yak.ngrok-free.app/contact"));
            }
        }

        [RelayCommand]
        private void Restart()
        {
            string startpath = System.IO.Directory.GetCurrentDirectory();
            System.Diagnostics.Process.Start(startpath + "\\CarpToolkit.Desktop.exe");
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
            {
                desktopApp.Shutdown();
            }
        }
    }
}
