using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace p1eXu5.Wpf.Mvvm.Commands
{

    using Contracts;
    using Extensions;

    public class MvvmAsyncCommand : IAsyncCommand
    {
        private readonly Func< object, Task > _execute;
        private readonly Predicate< object > _canExecute;
        private readonly IErrorHandler _errorHandler;

        private int _isExecuting = 0;

        public MvvmAsyncCommand ( Func< object, Task > execute, Predicate< object > canExecute = null, IErrorHandler errorHandler = null )
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), @"execute cannot be null.");
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public event EventHandler CanExecuteChanged;



        public bool CanExecute ( object parameter )
        {
            return _isExecuting == 0 && (_canExecute?.Invoke( parameter ) ?? true);
        }


        public async Task ExecuteAsync ( object parameter )
        {
            if ( CanExecute( parameter ) ) 
            {
                if ( Interlocked.CompareExchange( ref _isExecuting, 1, 0 ) == 1 ) return;

                try {
                    await _execute( parameter );
                }
                finally {
                    Volatile.Write( ref _isExecuting, 0 );
                }
            }
        }

        public void RiseCanExecuteChanged ()
        {
            CanExecuteChanged?.Invoke( this, EventArgs.Empty );
        }

        public Task ExecuteAsync ()
        {
            return ExecuteAsync( null );
        }


        #region Explicit Implementation

        void ICommand.Execute ( object parameter )
        {
            ExecuteAsync( parameter ).FireAndForgetSafeAsync( _errorHandler );
        }

        #endregion
    }
}
