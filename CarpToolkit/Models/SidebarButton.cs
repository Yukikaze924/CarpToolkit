using CarpToolkit.ViewModels;
using System;

namespace CarpToolkit.Models
{
    public class SidebarButton
    {
        public string Label { get; set; }
        public ViewModelBase ModelType { get; set; }
        public string Icon { get; set; }
        public bool CreateNewInstance { get; set; } = true;

        public SidebarButton(string label, ViewModelBase type, string icon)
        {
            Label = label;
            ModelType = type;
            Icon = icon;
        }

        public SidebarButton(string label, ViewModelBase type, string icon, bool createNewInstance)
        {
            Label = label;
            ModelType = type;
            Icon = icon;
            CreateNewInstance = createNewInstance;
        }
    }
}
