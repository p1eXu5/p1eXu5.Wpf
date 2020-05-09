﻿
namespace p1eXu5.Wpf.MvvmBaseLibrary
{
    public interface IDialogRepository
    {
        void Register< TViewModel, TView > () 
            where TView : IDialog, new()
            where TViewModel : IDialogCloseRequested;

        IDialog GetView< TViewModel > ( TViewModel viewModel )
            where TViewModel : IDialogCloseRequested;
    }
}
