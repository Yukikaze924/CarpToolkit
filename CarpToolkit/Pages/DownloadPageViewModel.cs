using CarpToolkit.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace CarpToolkit.Pages
{
    public partial class DownloadPageViewModel : ViewModelBase
    {
        [RelayCommand]
        private void Exit()
        {
            CurrentPageChanged?.Invoke(null, new ToolsViewModel());
        }
    }
}
