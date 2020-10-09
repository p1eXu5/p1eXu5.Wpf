using System.Windows;

namespace p1eXu5.Wpf.Mvvm.Contracts
{
    public interface IDialog
    {
        Window Owner { get; set; }
        object DataContext { get; set; }
        bool? ShowDialog ();
        bool? DialogResult { get; set; }
    }
}
