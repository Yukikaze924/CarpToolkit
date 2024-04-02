using Avalonia.Controls;
using Avalonia.Interactivity;
using CarpToolkit.Helpers;
using CarpToolkit.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CarpToolkit.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            AddHandler(Button.ClickEvent, OnNavButtonClick);
        }

        private void OnNavButtonClick(object? sender, RoutedEventArgs args)
        {
            if (args.Source is Button b && b.DataContext is HomeNavPageViewModel homeViewModel)
            {
                homeViewModel.Navigate();
                args.Handled = true;
            }
        }

        private void Border_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 800)
            {
                JumbotronImage.IsVisible = false;
                e.Handled = true;
                return;
            }
            else if (e.NewSize.Width > 800)
            {
                JumbotronImage.IsVisible= true;
            }
        }
    }
}
