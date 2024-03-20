using CarpToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace CarpToolkit.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    public static EventHandler<ViewModelBase>? CurrentPageChanged;

    public static EventHandler<string>? PaneLocationChanged;
}
