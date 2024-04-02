using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CarpToolkit.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarpToolkit.ViewModels
{
    partial class HomeViewModel : ViewModelBase
    {
        public List<HomeNavPageViewModel> Pages { get; set; } = new List<HomeNavPageViewModel>
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

        [ObservableProperty]
        private ObservableCollection<ArticleModel>? _articals;
        [ObservableProperty]
        private ObservableCollection<PostDetail> _posts = [];


        public HomeViewModel()
        {
            Articals = HttpHelper.GetLatestPost();
            if (Articals is not null)
            {
                foreach (var article in Articals)
                {
                    var avatar = ImageHelper.LoadFromWeb(new Uri(article.cover));
                    Posts.Add(new PostDetail(article.title, article.subtitle, avatar!));
                }
            }
        }


        [RelayCommand]
        private void LearnMoreButtonClicked()
        {
            new HomeNavPageViewModel()
            {
                NavigateURI = new Uri("https://partyanimals.com/")
            }.Navigate();
        }

        [RelayCommand]
        private void BuyOnSteamButtonClicked()
        {
            new HomeNavPageViewModel()
            {
                NavigateURI = new Uri("https://store.steampowered.com/app/1260320/Party_Animals/")
            }.Navigate();
        }
    }

    public class PostDetail
    {
        public PostDetail(string title, string description, Bitmap image)
        {
            Title = title;
            Description = description;
            Image = image;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }
    }

    public class ArticleModel
    {
        public int id { get; set; }
        public int date { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string category { get; set; }
        public string cover { get; set; }
        public string chapter { get; set; }
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
