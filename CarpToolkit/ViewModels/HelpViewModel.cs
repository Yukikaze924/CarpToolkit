using Avalonia.Controls;
using CarpToolkit.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarpToolkit.ViewModels
{
    public partial class HelpViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isExpanded = false;

        [ObservableProperty]
        private ObservableCollection<DependencyDetail> _dependencyDetails = new()
        {
            new DependencyDetail("Avalonia", "https://avaloniaui.net/"),
            new DependencyDetail("CommunityToolkit.Mvvm", "https://github.com/CommunityToolkit/dotnet"),
            new DependencyDetail("FluentAvalonia", "https://github.com/amwx/FluentAvalonia"),
            new DependencyDetail("Newtonsoft.Json", "https://www.newtonsoft.com/json"),
            new DependencyDetail("Serilog", "https://serilog.net/")
        };


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
    }

    public class DependencyDetail
    {
        public DependencyDetail(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }

        public async void Navigate()
        {
            if (Design.IsDesignMode)
                return;

            try
            {
                Process.Start(new ProcessStartInfo(Url)
                { UseShellExecute = true, Verb = "open" });
            }
            catch
            {
                await DialogHelper.ShowUnableToOpenLinkDialog(new Uri(Url));
            }
        }
    }
}
