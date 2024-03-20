using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CarpToolkit.ViewModels;
using CarpToolkit.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace CarpToolkit;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        Ioc.Default.ConfigureServices(ConfigureServices());

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();

        SettingsHelper.ChangeThemeVariantByString(SettingsHelper.Settings.AppTheme);
        Ioc.Default.GetRequiredService<ILogger>().Information("Application Initialization Completed\n"+
                                                              $"Platform: {System.Environment.OSVersion.VersionString}\n"+
                                                              $"Description: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}\n"+
                                                              $"Architecture: {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString()}");
    }

    public App()
    {
        SettingsHelper.init("preferences.json", new Settings(true, "Default", "Left"));
        CacheHelper.init("cache.json", new Cache(true)
        {
            isLoggedIn = false
        });
        SystemHelper.init();
    }

    private static IServiceProvider ConfigureServices()
    {
        var collection = new ServiceCollection();

        collection.AddTransient<MainViewModel>();
        //collection.AddTransient<SettingsViewModel>();
        collection.AddTransient<AccountViewModel>();

        collection.AddSingleton<ILogger>(new LoggerConfiguration().WriteTo.File(@"CarpToolkit.log").CreateLogger());

        return collection.BuildServiceProvider();
    }
}
