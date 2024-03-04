using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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
        private string? _currentAppTheme = "Default";

        partial void OnCurrentAppThemeChanged(string? value)
        {
            if (value != null)
            {
                switch (value)
                {
                    case "Default":
                        App.Current!.RequestedThemeVariant = ThemeVariant.Default;
                        break;

                    case "Light":
                        App.Current!.RequestedThemeVariant = ThemeVariant.Light;
                        break;

                    case "Dark":
                        App.Current!.RequestedThemeVariant = ThemeVariant.Dark;
                        break;
                }
            }
            
        }
    }
}
