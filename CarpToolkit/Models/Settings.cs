using CarpToolkit.Services;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;

namespace CarpToolkit.Models
{
    public class Settings
    {
        public string StoragePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string AppTheme { get; set; } = "Default";
        public int Width { get; set; } = 1114;
        public int Height { get; set; } = 700;
        public bool UseDefault { get; set; } = true;
        public bool IsPaneOpen { get; set; } = false;
        public string PaneLocation { get; set; } = "Left";
    }
}
