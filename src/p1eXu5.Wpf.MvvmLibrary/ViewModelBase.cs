using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace p1eXu5.Wpf.MvvmLibrary
{
    public abstract class ViewModelBase : MvvmBaseLibrary.ViewModelBase, IDataErrorInfo
    {
        protected override string OnValidate(string propertyName)
        {
            ValidationContext validationContext = new ValidationContext(this) {DisplayName = propertyName};
            var errorsCollection = new Collection<ValidationResult>();

            if (Validator.TryValidateProperty(this, validationContext, errorsCollection)) {

                return errorsCollection.First().ErrorMessage;
            }

            return null;
        }
    }
}
