using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Threading;
using CarpToolkit.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using CarpToolkit.Helpers;
using System;
using System.IO;

namespace CarpToolkit.Pages
{
    public partial class DownloadPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<FileDetail> _fileDetails = [];
        [RelayCommand]
        private void ClearHistory()
        {
            FileDetails.Clear();
        }


        [ObservableProperty]
        private string _address;
        [RelayCommand]
        private async Task DownloadAsync()
        {
            if (Address == null)
            {
                DialogHelper.ShowResultDialog("Error!", "You have to submit a valid Address!");
                return;
            }

            string downloadFolder;
            string filename;
            try
            {
                filename = Path.GetFileName(new Uri(Address).AbsolutePath);
                downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            }
            catch
            {
                DialogHelper.ShowResultDialog("Error!", "Failed to obtain user download path directory!");
                return;
            }
            string filepath = $@"{downloadFolder}\{filename}";

            var td = new TaskDialog
            {
                Title = "Carp Toolkit",
                ShowProgressBar = true,
                IconSource = new SymbolIconSource { Symbol = Symbol.Download },
                SubHeader = "Downloading",
                Content = "Please wait while your file downloads",
                Buttons =
                {
                    TaskDialogButton.CancelButton
                }
            };

            td.Opened += async (s, e) =>
            {
                await Task.Run(async () =>
                {
                    TaskDialogProgressState state = TaskDialogProgressState.Indeterminate;

                    td.SetProgressBarState(0, state);

                    await HttpHelper.DownloadFile(Address, filepath);

                    Dispatcher.UIThread.Post(() => { td.Hide(TaskDialogStandardResult.OK); });
                });
            };

            // Don't forget to set the XamlRoot!!
            var app = Application.Current.ApplicationLifetime;
            if (app is IClassicDesktopStyleApplicationLifetime desktop)
            {
                td.XamlRoot = desktop.MainWindow;
            }
            else if (app is ISingleViewApplicationLifetime single)
            {
                td.XamlRoot = TopLevel.GetTopLevel(single.MainView);
            }
            var result = await td.ShowAsync();

            switch (result)
            {
                case TaskDialogStandardResult.OK:
                    //DialogHelper.ShowResultDialog("File has been downloaded!", filepath);
                    FileDetails.Add(new FileDetail(filename, Address, DateTime.Now, GetFileFormat(filename)));
                    break;
            }
        }
        private string GetFileFormat(string fileName)
        {
            // 获取文件扩展名的起始位置
            int dotIndex = fileName.LastIndexOf('.');

            if (dotIndex != -1)
            {
                // 提取文件扩展名部分
                string fileExtension = fileName.Substring(dotIndex + 1);

                return fileExtension;
            }
            else
            {
                return "File";
            }
        }


        [RelayCommand]
        private void Exit()
        {
            CurrentPageChanged?.Invoke(null, new ToolsViewModel());
        }
    }

    public class FileDetail
    {
        public FileDetail(string name, string link, DateTime date, string format)
        {
            Name = name;
            Link = link;
            Date = date;
            Format = format;
        }

        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public string Format { get; set; }
    }
}
