using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Serilog;
using System;
using System.Diagnostics;

namespace CarpToolkit.Pages
{
    public partial class DownloadPageView : UserControl
    {
        public DownloadPageView()
        {
            InitializeComponent();
        }

        private void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                var row = (FileDetail)dataGrid.SelectedItem;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"\Downloads\{row.Name}";
                Process.Start(new ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }
    }
}
