using System.ComponentModel;

namespace p1eXu5.Wpf.MvvmBaseLibrary
{
    public abstract class NotifyingObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged( string propertyName = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
