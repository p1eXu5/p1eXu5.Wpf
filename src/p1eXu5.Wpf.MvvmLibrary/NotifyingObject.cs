using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace p1eXu5.Wpf.MvvmLibrary
{
    public abstract class NotifyingObject : MvvmBaseLibrary.NotifyingObject, INotifyPropertyChanged
    {
        protected override void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            base.OnPropertyChanged( propertyName );
        }
    }
}
