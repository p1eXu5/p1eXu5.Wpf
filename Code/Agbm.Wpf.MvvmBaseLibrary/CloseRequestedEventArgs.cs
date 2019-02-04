using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agbm.Wpf.MvvmBaseLibrary
{
    public class CloseRequestedEventArgs : EventArgs
    {
        public CloseRequestedEventArgs ( bool? dialogResult )
        {
            DialogResult = dialogResult;
        }

        public bool? DialogResult { get; }
    }
}
