using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CarpToolkit.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace CarpToolkit.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private Models.App app = Ioc.Default.GetRequiredService<ConfigService>().AppConfig.App;


    /// <summary>
    /// Windows related bindings
    /// </summary>
    [ObservableProperty]
    private int _width;
    [ObservableProperty]
    private int _height;


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
        new SidebarButton("Home", new HomeViewModel(), "Home", false),
        new SidebarButton("Tools", new ToolsViewModel(), "Library", false),
    };
    [ObservableProperty]
    private ObservableCollection<SidebarButton> _sidebarFooterButtons = new()
    {
        new SidebarButton("Account", new AccountViewModel(), "Contact"),
        new SidebarButton("Settings", new SettingsViewModel(), "Settings", false),
        new SidebarButton("Help", new HelpViewModel(), "Help", false),
    };


    /// <summary>
    /// Pane related bindings
    /// </summary>
    [ObservableProperty]
    private bool _isPaneOpen;
    [ObservableProperty]
    private string _paneLocation;
    [ObservableProperty]
    private int _paneWidth = 240;



    public MainViewModel()
    {
        Settings settings = SettingsHelper.Load()!;
        Width = settings.Width;
        Height = settings.Height;
        IsPaneOpen = settings.IsPaneOpen;
        PaneLocation = settings.PaneLocation;

        SelectedButton = SidebarButtons[0];

        CurrentPageChanged += (sender, page) => CurrentPage = page;
        PaneLocationChanged += (sender, args) => PaneLocation = settings.PaneLocation;
    }
}
