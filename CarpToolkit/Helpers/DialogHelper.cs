﻿using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using Avalonia;
using Avalonia.VisualTree;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls.Primitives;
using System.Threading.Tasks;
using System;

namespace CarpToolkit.Helpers
{
    public class DialogHelper
    {
        public static async Task ShowUnableToOpenLinkDialog(Uri uri)
        {
            var copyLinkButton = new TaskDialogCommand
            {
                Text = "Copy Link",
                IconSource = new SymbolIconSource { Symbol = Symbol.Link },
                Description = uri.ToString(),
                ClosesOnInvoked = false
            };

            var td = new TaskDialog
            {
                Content = "It looks like your system experienced some process issues " +
                "and we are unable to open a link.",
                SubHeader = "Oh No!",
                Commands =
                {
                    copyLinkButton
                },
                Buttons =
                {
                    TaskDialogButton.OKButton
                },
                IconSource = new SymbolIconSource { Symbol = Symbol.ImportantFilled }
            };

            copyLinkButton.Click += async (s, __) =>
            {
                await ClipboardHelper.SetTextAsync(uri.ToString());

                var flyout = new Flyout
                {
                    Content = "Copied!"
                };

                var comHost = td.FindDescendantOfType<TaskDialogCommandHost>();

                FlyoutBase.SetAttachedFlyout(comHost, flyout);
                FlyoutBase.ShowAttachedFlyout(comHost);

                DispatcherTimer.RunOnce(() => flyout.Hide(), TimeSpan.FromSeconds(1));
            };

            var app = Application.Current.ApplicationLifetime;
            if (app is IClassicDesktopStyleApplicationLifetime desktop)
            {
                td.XamlRoot = desktop.MainWindow;
            }
            else if (app is ISingleViewApplicationLifetime single)
            {
                td.XamlRoot = TopLevel.GetTopLevel(single.MainView);
            }

            await td.ShowAsync(true);
        }

        public static async void ShowResultDialog(string header, string subheader)
        {
            var dialog = new ContentDialog()
            {
                Title = header,
                CloseButtonText = "Close",
            };

            dialog.Content = new TextBlock()
            {
                Text = subheader
            };

            await dialog.ShowAsync();
        }
    }
}