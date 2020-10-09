using System;
using System.Windows.Input;

namespace p1eXu5.Wpf.Mvvm.Dialog
{
    using Contracts;
    using Commands;


    public abstract class DialogViewModel : IDialogCloseRequested
    {
        public ICommand OkCommand => new MvvmCommand(p => { OnDialogRequestClose(this, new CloseRequestedEventArgs(true));});
        public ICommand CancelCommand => new MvvmCommand(p => { OnDialogRequestClose(this, new CloseRequestedEventArgs(false));});

        public virtual void OnDialogRequestClose(object sender, CloseRequestedEventArgs args)
        {
            CloseRequested?.Invoke(sender, args);
        }

        public event EventHandler< CloseRequestedEventArgs > CloseRequested;
    }
}
