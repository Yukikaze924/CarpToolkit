using Avalonia.Controls;
using Avalonia.Interactivity;
using CarpToolkit.ViewModels;
using System;
using System.Diagnostics;

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
    }
}
