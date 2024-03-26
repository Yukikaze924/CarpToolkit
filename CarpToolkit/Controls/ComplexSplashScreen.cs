﻿using Avalonia.Media;
using FluentAvalonia.UI.Windowing;
using System.Threading;
using System.Threading.Tasks;

namespace CarpToolkit.Controls
{
    public class ComplexSplashScreen : IApplicationSplashScreen
    {
        public ComplexSplashScreen()
        {
            SplashScreenContent = new DemoComplexSplashScreen();
        }

        public string AppName { get; }
        public IImage AppIcon { get; }
        public object SplashScreenContent { get; }

        // To avoid too quickly transitioning away from the splash screen, you can set a minimum
        // time to hold before loading the content, value is in Milliseconds
        public int MinimumShowTime { get; set; }


        // Place your loading tasks here. NOTE, this is already called on a background thread, so
        // if any UI thread work needs to be done, use Dispatcher.UIThread.Post or .InvokeAsync
        public async Task RunTasks(CancellationToken token)
        {
            await ((DemoComplexSplashScreen)SplashScreenContent).InitApp();
        }
    }
}
