using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace CarpToolkit.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    public static EventHandler<ViewModelBase>? CurrentPageChanged;
}
