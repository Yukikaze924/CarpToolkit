using Avalonia;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;

namespace CarpToolkit.Models
{
    public class ToolListItem
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public IconSource Icon { get; set; }
        public RelayCommand InvokeCommand { get; set; }

        public ToolListItem(string header, string description, string iconKey, RelayCommand invokeCommand)
        {
            Header = header;
            Description = description;
            Application.Current.Resources.TryGetResource(iconKey, null, out var icon);
            Icon = (IconSource)icon;
            InvokeCommand = invokeCommand;
        }
    }
}
