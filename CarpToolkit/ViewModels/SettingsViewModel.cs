using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CarpToolkit.Helpers;
using CarpToolkit.Models;
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
        private string? _currentAppTheme;
        partial void OnCurrentAppThemeChanged(string? value)
        {
            SettingsHelper.Settings.AppTheme = value;
            SettingsHelper.Save();
            SettingsHelper.ChangeThemeVariantByString(value);
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
        private string _paneLocation;
        partial void OnPaneLocationChanged(string value)
        {
            SettingsHelper.Settings.PaneLocation = value;
            SettingsHelper.Save();

            if (value == "Top")
            {
                IsPaneOpenOptionEnable = false;
            }
            else
            {
                IsPaneOpenOptionEnable = true;
            }

            IsOptionsChanged = true;
        }


        /// <summary>
        /// Bindings for pane's state of opening.
        /// </summary>
        [ObservableProperty]
        private bool _isPaneOpenOptionEnable;
        [ObservableProperty]
        private bool _isPaneOpen;
        partial void OnIsPaneOpenChanged(bool value)
        {
            SettingsHelper.Settings.IsPaneOpen = value;
            SettingsHelper.Save();
            IsOptionsChanged = true;
        }


        /// <summary>
        /// Bindings for Windows size
        /// </summary>
        [ObservableProperty]
        private bool _useDefault;
        [ObservableProperty]
        private int _width;
        [ObservableProperty]
        private int _height;
        partial void OnUseDefaultChanged(bool value)
        {
            Settings settings = SettingsHelper.Settings;
            if (value==true)
            {
                settings.Width = 960;
                settings.Height = 600;
            }
            else if(value==false)
            {
                settings.Width = Width;
                settings.Height = Height;
            }

            settings.UseDefault = value;
            SettingsHelper.Save();

            IsOptionsChanged = true;
        }
        partial void OnWidthChanged(int value)
        {
            if (UseDefault)
            {
                return;
            }
            else
            {
                Settings settings = SettingsHelper.Settings;
                settings.Width = value;
                SettingsHelper.Save();

                IsOptionsChanged = true;
            }
        }
        partial void OnHeightChanged(int value)
        {
            if (UseDefault)
            {
                return;
            }
            else
            {
                Settings settings = SettingsHelper.Settings;
                settings.Height = value;
                SettingsHelper.Save();

                IsOptionsChanged = true;
            }
        }



        public SettingsViewModel()
        {
            Settings settings = SettingsHelper.Load()!;
            UseDefault = settings.UseDefault;
            if (UseDefault)
            {
                Width = 960;
                Height = 600;
            }
            else if (!UseDefault)
            {
                Width = settings.Width;
                Height = settings.Height;
            }
            CurrentAppTheme = settings.AppTheme;
            PaneLocation = settings.PaneLocation;
            IsPaneOpen = settings.IsPaneOpen;

            if (PaneLocation == "Top")
            {
                IsPaneOpenOptionEnable = false;
            }
            else
            {
                IsPaneOpenOptionEnable= true;
            }

            /*
             * 由于在构造函数中赋值会触发 PropertyChanged(), 
             * 导致 IsOptionsChanged = true
             * 所以我在构造函数尾部重新将 IsOptionsChanged 恢复为false
             */
            IsOptionsChanged = false;
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
