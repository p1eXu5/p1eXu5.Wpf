using System.Windows;

namespace p1eXu5.Wpf.MvvmBaseLibrary
{
    public interface IDialog
    {
        Window Owner { get; set; }
        object DataContext { get; set; }
        bool? ShowDialog ();
        bool? DialogResult { get; set; }
    }
}
