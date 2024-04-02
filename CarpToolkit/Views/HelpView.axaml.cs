using Avalonia.Controls;
using Avalonia.Interactivity;
using CarpToolkit.ViewModels;

namespace CarpToolkit.Views
{
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
        }

        private void Button_Click(object? sender, RoutedEventArgs args)
        {
            if (args.Source is Button b && b.DataContext is DependencyDetail dependency)
            {
                dependency.Navigate();
                args.Handled = true;
            }
        }
    }
}
