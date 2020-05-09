using System;
using System.Threading.Tasks;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace p1eXu5.Wpf.MvvmLibrary
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync ( this Task task, IErrorHandler handler = null )
        {
            try {
                await task;
            }
            catch( Exception ex ) {

                handler?.HandleError( ex );
            }
        }
    }
}
