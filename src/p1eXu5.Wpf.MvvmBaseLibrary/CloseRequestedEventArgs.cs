using System;

namespace p1eXu5.Wpf.MvvmBaseLibrary
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
