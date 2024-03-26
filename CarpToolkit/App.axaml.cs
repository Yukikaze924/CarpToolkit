using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CarpToolkit.Services;
using CarpToolkit.ViewModels;
using CarpToolkit.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

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
                                                              $"Platform: {Environment.OSVersion.VersionString}\n"+
                                                              $"Description: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}\n"+
                                                              $"Architecture: {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture}\n");
    }

    public static string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\CarpToolkit";

    public App()
    {
        Ioc.Default.ConfigureServices(ConfigureServices());

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        SettingsHelper.init($"{path}\\preferences.json", new Settings());
        CacheHelper.init($"{path}\\cache.dll", new Cache());
        SystemHelper.init();
    }

    private static IServiceProvider ConfigureServices()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<ILogger>(new LoggerConfiguration().WriteTo.File($"{path}\\CarpToolkit.log").CreateLogger());
        collection.AddSingleton(new ConfigService
        {
            Path = $"{path}\\appconfig.dll",
            AppConfig = new AppConfig
            {
                Host = new Host("hakuryuu25500.top", "http://hakuryuu25500.top", new Port(80, 8000, 3306))
            },
        }.Build());

        return collection.BuildServiceProvider();
    }
}
