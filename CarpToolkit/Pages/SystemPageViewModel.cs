using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CarpToolkit.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace CarpToolkit.Pages
{
    public partial class SystemPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _deviceName;
        [ObservableProperty]
        private string _processor;
        [ObservableProperty]
        private string _memory;

        [ObservableProperty]
        private ObservableCollection<VideoCard>? _videoCards = new ObservableCollection<VideoCard>();
        [ObservableProperty]
        private VideoCard? _selectedVideoCard;
        [ObservableProperty]
        private string? _videoCardName;
        [ObservableProperty]
        private string? _videoCardRAM;

        partial void OnSelectedVideoCardChanged(VideoCard value)
        {
            VideoCardName = value.VideoCardName;
            VideoCardRAM = value.VideoCardRAM.ToString() + " GB";
        }

        public SystemPageViewModel()
        {
            DeviceName = Environment.MachineName;
            Processor = SystemHelper.ProcessorList[0];
            Memory = SystemHelper.TotalMemory.ToString()+" GB";

            for (int i=0; i<SystemHelper.VideoCardList.Count; i++)
            {
                VideoCards.Add(new VideoCard(SystemHelper.VideoCardList[i], SystemHelper.VideoCardRAMList[i]));
                VideoCardName = SystemHelper.VideoCardList[i];
                VideoCardRAM = SystemHelper.VideoCardRAMList[i].ToString() + " GB";
            }
            SelectedVideoCard = VideoCards[0];
        }

        [RelayCommand]
        private void Exit()
        {
            CurrentPageChanged?.Invoke(null, new ToolsViewModel());
        }
    }
}
