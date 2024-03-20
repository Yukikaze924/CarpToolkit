using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace CarpToolkit.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Content related bindings
    /// </summary>
    [ObservableProperty]
    private ViewModelBase _currentPage = new HomeViewModel();
    [ObservableProperty]
    private SidebarButton? _selectedButton;
    partial void OnSelectedButtonChanged(SidebarButton? value)
    {
        if (value is not null && value.CreateNewInstance == true)
        {
            var instance = Activator.CreateInstance(value.ModelType.GetType());
            if (instance == null) return;

            CurrentPage = (ViewModelBase)instance;
        }
        else if (!value.CreateNewInstance)
        {
            CurrentPage = value.ModelType;
        }
    }
    [ObservableProperty]
    private ObservableCollection<SidebarButton> _sidebarButtons = new()
    {
        new SidebarButton("Home", new HomeViewModel(), "Home"),
        new SidebarButton("Tools", new ToolsViewModel(), "Library"),
    };
    [ObservableProperty]
    private ObservableCollection<SidebarButton> _sidebarFooterButtons = new()
    {
        new SidebarButton("Account", new AccountViewModel(), "Contact"),
        new SidebarButton("Settings", new SettingsViewModel(), "Settings", false),
    };

    /// <summary>
    /// Pane related bindings
    /// </summary>
    [ObservableProperty]
    private bool _isPaneOpen = SettingsHelper.Load()!.IsPaneOpen;
    [ObservableProperty]
    private string _paneLocation = SettingsHelper.Load()!.PaneLocation;
    [ObservableProperty]
    private int _paneWidth = 240;

    public MainViewModel()
    {
        SelectedButton = SidebarButtons[0];

        CurrentPageChanged += (sender, page) => CurrentPage = page;
        PaneLocationChanged += (sender, args) =>
        {
            PaneLocation = SettingsHelper.Load().PaneLocation;
        };
    }
}
