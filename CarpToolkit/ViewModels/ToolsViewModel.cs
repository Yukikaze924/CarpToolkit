using CarpToolkit.Models;
using CarpToolkit.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CarpToolkit.ViewModels
{
    public partial class ToolsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<ToolListItem> _toolListItems = new()
        {
            new ToolListItem("Markdown", "Amazing tool for you to Read, Edit and Manage your Markdown Document", "MiscPageIcon", new RelayCommand(() =>
            {
                CurrentPageChanged?.Invoke(null, new MarkdownPageViewModel());
            })),
            new ToolListItem("System", "Easily change your system configuration", "SystemIcon", new RelayCommand(() =>
            {
                CurrentPageChanged?.Invoke(null, new SystemPageViewModel());
            })),
            new ToolListItem("Download", "Multi-Thread download manager!", "DownloadIcon", new RelayCommand(() =>
            {
                CurrentPageChanged?.Invoke(null, new DownloadPageViewModel());
            }))
        };
    }
}
