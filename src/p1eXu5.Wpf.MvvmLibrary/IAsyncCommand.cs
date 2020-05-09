
using System.Threading.Tasks;
using System.Windows.Input;

namespace p1eXu5.Wpf.MvvmLibrary
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync ( object obj );
    }
}
