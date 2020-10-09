using System;
using System.Collections.Generic;
using System.Windows;

namespace p1eXu5.Wpf.Mvvm.Dialog
{
    using Contracts;

    public class DialogRepository : IDialogRepository
    {
        private readonly Dictionary<Type, Type> _repository;
        private readonly Window _owner;

        public DialogRepository( Window owner )
        {
            _owner = owner ?? throw new ArgumentNullException();

            _repository = new Dictionary<Type, Type>();
        }

        public void Register< TViewModel, TView >() 
            where      TView : IDialog, new()
            where TViewModel : IDialogCloseRequested
        {
            _repository[ typeof( TViewModel ) ] = typeof( TView );
        }

        public IDialog GetView< TViewModel >( TViewModel viewModel )
            where TViewModel : IDialogCloseRequested
        {
            if ( _repository.TryGetValue( typeof( TViewModel ), out var viewType ) ) {

                IDialog view = ( IDialog )Activator.CreateInstance( viewType );

                view.DataContext = viewModel;
                view.Owner = _owner;

                void OnCloseRequestEventHandler ( object sender, CloseRequestedEventArgs args )
                {
                    viewModel.CloseRequested -= OnCloseRequestEventHandler;
                    view.DialogResult = args.DialogResult;
                }

                viewModel.CloseRequested += OnCloseRequestEventHandler;

                return view;
            }

            return null;
        }
    }
}
