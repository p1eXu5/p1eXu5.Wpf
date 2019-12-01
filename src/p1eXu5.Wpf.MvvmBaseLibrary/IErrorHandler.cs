using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1eXu5.Wpf.MvvmBaseLibrary
{
    public interface IErrorHandler
    {
        void HandleError ( Exception ex );
    }
}
