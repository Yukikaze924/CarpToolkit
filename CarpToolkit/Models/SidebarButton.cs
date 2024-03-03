using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpToolkit.Models
{
    public class SidebarButton
    {
        public SidebarButton(string label, Type type, string icon)
        {
            Label = label;
            ModelType = type;
            Icon = icon;
            //Application.Current!.TryFindResource(icon, out var res);
            //Icon = (StreamGeometry)res!;
        }

        public string Label { get; set; }
        public Type ModelType { get; set; }
        public string Icon { get; set; }
    }
}
