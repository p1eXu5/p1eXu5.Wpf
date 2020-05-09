using System;
using System.Windows.Input;

namespace p1eXu5.Wpf.MvvmBaseLibrary
{
    public interface IDialogCloseRequested
    {
        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }

        event EventHandler< CloseRequestedEventArgs > CloseRequested;
    }
}
