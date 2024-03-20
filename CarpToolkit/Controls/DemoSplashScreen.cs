using Avalonia.Media;
using FluentAvalonia.UI.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarpToolkit.Controls
{
    public class DemoSplashScreen : IApplicationSplashScreen
    {
        public string AppName { get; init; }
        public IImage AppIcon { get; init; }
        public object SplashScreenContent { get; init; }

        // To avoid too quickly transitioning away from the splash screen, you can set a minimum
        // time to hold before loading the content, value is in Milliseconds
        public int MinimumShowTime { get; set; }

        // Place your loading tasks here. NOTE, this is already called on a background thread, so
        // if any UI thread work needs to be done, use Dispatcher.UIThread.Post or .InvokeAsync
        public Task RunTasks(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
