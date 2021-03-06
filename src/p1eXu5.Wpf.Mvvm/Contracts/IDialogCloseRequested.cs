﻿using System;
using System.Windows.Input;

namespace p1eXu5.Wpf.Mvvm.Contracts
{
    using Dialog;

    public interface IDialogCloseRequested
    {
        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }

        event EventHandler< CloseRequestedEventArgs > CloseRequested;
    }
}
