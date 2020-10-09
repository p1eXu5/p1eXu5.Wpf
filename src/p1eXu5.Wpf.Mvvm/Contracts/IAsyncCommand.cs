
using System.Threading.Tasks;
using System.Windows.Input;

namespace p1eXu5.Wpf.Mvvm.Contracts
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync ( object obj );
    }
}
