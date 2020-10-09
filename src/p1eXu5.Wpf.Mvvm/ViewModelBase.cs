using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace p1eXu5.Wpf.Mvvm
{
    public abstract class ViewModelBase : NotifyingObject, IDataErrorInfo
    {
        public string this[string columnName] => OnValidate(columnName);

        public string Error => throw new NotImplementedException($"{nameof(IDataErrorInfo)} shouldn't implement Error property for WPF error binding.");

        protected virtual string OnValidate(string propertyName)
        {
            return "";
        }

        protected static void Observe<TModel, TViewModel> (ReadOnlyObservableCollection<TModel> observable, 
                                                               ObservableCollection<TViewModel> observing, 
                                                                        Func<TViewModel,TModel> func)
        {
            ((INotifyCollectionChanged) observable).CollectionChanged += (s, e) =>
                {
                    if (e.NewItems?[0] != null) {
                        observing.Add ((TViewModel)Activator.CreateInstance (typeof(TViewModel), new[] { (TModel)e.NewItems[0] }));
                    }

                    if (e.OldItems?[0] != null) {
                        observing.Remove (observing.First(vm => ReferenceEquals (func(vm), e.OldItems[0])));
                    }

                    if (e.NewItems?[0] == null && e.OldItems?[0] == null) {
                        observing.Clear();
                    }
                };
        }

        protected static void Observe<TModel, TViewModel, TParam> (ReadOnlyObservableCollection<TModel> observable, 
                                                                   ObservableCollection<TViewModel> observing, 
                                                                   Func<TViewModel,TModel> func,
                                                                   TParam param
        )
        {
            ((INotifyCollectionChanged) observable).CollectionChanged += (s, e) =>
            {

                if ( e.NewItems?[ 0 ] != null ) {
                    observing.Add( ( TViewModel )Activator.CreateInstance( typeof( TViewModel ), new object[] { ( TModel )e.NewItems[ 0 ], param } ) );
                }

                if ( e.OldItems?[ 0 ] != null ) {
                    observing.Remove( observing.First( vm => ReferenceEquals( func( vm ), e.OldItems[ 0 ] ) ) );
                }

                if ( e.NewItems?[ 0 ] == null && e.OldItems?[ 0 ] == null ) {
                    try {
                        observing.Clear();
                    }
                    catch ( Exception ) {
                        ;
                    }
                }

            };
        }
    }
}
