using Avalonia.Controls;
using CarpToolkit.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpToolkit.ViewModels
{
    partial class HomeViewModel : ViewModelBase
    {
        public List<HomeNavPageViewModel> Pages { get; set; }
        public HomeViewModel()
        {
            Pages = new List<HomeNavPageViewModel>
            {
                new HomeNavPageViewModel("Documentation", new Uri("https://yukikaze924.github.io/"))
                {
                    ImageUri = "avares://CarpToolkit/Assets/Images/Documentation.png"
                },
                new HomeNavPageViewModel("Github Repo", new Uri("https://github.com/Yukikaze924/CarpToolkit"))
                {
                    ImageUri = "avares://CarpToolkit/Assets/Images/Github.png"
                },
                new HomeNavPageViewModel("Avalonia Repo", new Uri("https://www.github.com/AvaloniaUI/Avalonia"))
                {
                    ImageUri = "avares://CarpToolkit/Assets/Images/AvGithub.png"
                },
                new HomeNavPageViewModel("Fluent Design", new Uri("https://learn.microsoft.com/en-us/windows/apps/design/"))
                {
                    ImageUri = "avares://CarpToolkit/Assets/Images/FluentDesign.png"
                }
            };
        }
    }

    public class HomeNavPageViewModel
    {
        public HomeNavPageViewModel() { }

        public HomeNavPageViewModel(string title, Uri uri)
        {
            Title = title;
            NavigateURI = uri;
        }

        public string Title { get; set; }

        public string ImageUri { get; set; }

        public Uri NavigateURI { get; set; }

        public async void Navigate()
        {
            if (Design.IsDesignMode)
                return;

            try
            {
                Process.Start(new ProcessStartInfo(NavigateURI.ToString())
                { UseShellExecute = true, Verb = "open" });
            }
            catch
            {
                await DialogHelper.ShowUnableToOpenLinkDialog(NavigateURI);
            }
        }
    }
}
