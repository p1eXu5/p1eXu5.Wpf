using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace p1eXu5.Wpf.Mvvm.Extensions
{
    using Contracts;

    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync ( this Task task, IErrorHandler handler = null )
        {
            try {
                await task;
            }
            catch( Exception ex ) {

                Debug.WriteLine( ex.Message );
                handler?.HandleError( ex );
            }
        }
    }
}
