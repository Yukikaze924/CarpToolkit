﻿using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace CarpToolkit.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage = new HomeViewModel();

    [ObservableProperty]
    private bool _isPaneOpen = SettingsHelper.Load().IsPaneOpen;

    [ObservableProperty]
    private SidebarButton? _selectedButton;
    partial void OnSelectedButtonChanged(SidebarButton? value)
    {
        if (value is null) return;

        var instance = Activator.CreateInstance(value.ModelType);
        if (instance == null) return;

        CurrentPage = (ViewModelBase)instance;
    }

    [ObservableProperty]
    private ObservableCollection<SidebarButton> _sidebarButtons = new()
    {
        new SidebarButton("Home", typeof(HomeViewModel), "Home"),
        new SidebarButton("Tools", typeof(ToolsViewModel), "Library"),
    };

    public static EventHandler<ViewModelBase> CurrentPageChanged;

    public MainViewModel()
    {
        SelectedButton = SidebarButtons[0];

        CurrentPageChanged += (sender, page) => CurrentPage = page;
    }
}
