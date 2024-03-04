using CarpToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace CarpToolkit.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = false;
    [RelayCommand]
    private void ChangePaneState()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [ObservableProperty]
    private ViewModelBase _currentPage = new HomeViewModel();

    [ObservableProperty]
    private SidebarButton? _selectedButton;

    partial void OnSelectedButtonChanged(SidebarButton? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance == null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    private ObservableCollection<SidebarButton> SidebarButtons { get; } = new()
    {
        new SidebarButton("Home", typeof(HomeViewModel), "Home"),
        new SidebarButton("About", typeof(AboutViewModel), "Play"),
    };
}
