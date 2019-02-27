
using System.Threading.Tasks;
using System.Windows.Input;

namespace Agbm.Wpf.MvvmBaseLibrary
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync ( object obj );
        bool CanExecute ();
    }
}
