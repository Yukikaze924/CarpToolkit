using CarpToolkit.Controls;
using CarpToolkit.Helpers;
using CarpToolkit.Models;
using CarpToolkit.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using FluentAvalonia.UI.Windowing;

namespace CarpToolkit.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        if (CacheHelper.Load()!.isFirstTime)
        {
            SplashScreen = new ComplexSplashScreen();
            CacheHelper.Load()!.isFirstTime = false;
            CacheHelper.Save();
        }

        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        ClipboardHelper.init(this);
    }
}
