using Avalonia.Controls;
using CarpToolkit.Controls;
using CarpToolkit.Helpers;
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
            CacheHelper.Cache.isFirstTime = false;
            CacheHelper.Save();
        }

        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        ClipboardHelper.init(this);
    }
}
