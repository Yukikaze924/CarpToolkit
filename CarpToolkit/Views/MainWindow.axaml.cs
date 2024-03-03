using CarpToolkit.Models;
using CarpToolkit.ViewModels;
using FluentAvalonia.UI.Windowing;

namespace CarpToolkit.Views;

public partial class MainWindow : AppWindow
{
    //private bool _mouseDownForWindowMoving = false;
    //private PointerPoint _originalPoint;
    public MainWindow()
    {
        InitializeComponent();

        //AddHandler(PointerPressedEvent, InputElement_OnPointerPressed, RoutingStrategies.Tunnel);
        //AddHandler(PointerReleasedEvent, InputElement_OnPointerReleased, RoutingStrategies.Tunnel);
        //AddHandler(PointerMovedEvent, InputElement_OnPointerMoved, RoutingStrategies.Tunnel);
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        MainNavigationView.FooterMenuItems.Add(new SidebarButton("Settings", typeof(SettingsViewModel), "Settings"));
    }

    //private void InputElement_OnPointerMoved(object? sender, PointerEventArgs e)
    //{
    //    if (!_mouseDownForWindowMoving) return;

    //    PointerPoint currentPoint = e.GetCurrentPoint(this);
    //    Position = new PixelPoint(Position.X + (int)(currentPoint.Position.X - _originalPoint.Position.X),
    //        Position.Y + (int)(currentPoint.Position.Y - _originalPoint.Position.Y));
    //}
    //private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    //{
    //    if (WindowState == WindowState.Maximized || WindowState == WindowState.FullScreen) return;

    //    _mouseDownForWindowMoving = true;
    //    _originalPoint = e.GetCurrentPoint(this);
    //}

    //private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    //{
    //    _mouseDownForWindowMoving = false;
    //}
}
