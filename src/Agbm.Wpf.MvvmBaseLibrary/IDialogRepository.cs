using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agbm.Wpf.MvvmBaseLibrary
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
