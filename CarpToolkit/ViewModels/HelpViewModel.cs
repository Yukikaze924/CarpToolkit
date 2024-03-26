using CommunityToolkit.Mvvm.ComponentModel;

namespace CarpToolkit.ViewModels
{
    public partial class HelpViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isExpanded = true;
    }
}
