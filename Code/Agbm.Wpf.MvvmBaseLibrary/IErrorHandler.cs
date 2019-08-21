using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agbm.Wpf.MvvmBaseLibrary
{
    public interface IErrorHandler
    {
        void HandleError ( Exception ex );
    }
}
