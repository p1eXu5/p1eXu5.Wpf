using System;
using System.Collections.Generic;
using System.Windows;

namespace Agbm.Wpf.MvvmBaseLibrary
{
    public class ViewRepository : IDialogRepository
    {
        private readonly Dictionary<Type, Type> _repository;
        private readonly Window _owner;

        public ViewRepository( Window owner )
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

                viewModel.CloseRequested += OnCloseDialogHandler;

                return view;
            }

            return null;

            void OnCloseDialogHandler ( object s, CloseRequestedEventArgs e )
                {
                    viewModel.CloseRequested -= OnCloseDialogHandler;

                    ( ( IDialog )s ).DialogResult = e.DialogResult;
                }
        }
    }
}
