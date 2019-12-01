using System;
using System.ComponentModel;
using NUnit.Framework;

namespace p1eXu5.Wpf.MvvmBaseLibrary.Tests.UnitTests
{
    [TestFixture]
    public class ViewModelUnitTests
    {
        [Test]
        public void ViewModel_IsAbstract()
        {
            Assert.That(typeof(ViewModel).IsAbstract);
        }

        [Test]
        public void ViewModel_ByDefault_DerivesNotyfierObject()
        {
            var viewModel = GetViewModel();

            Assert.That(viewModel, Is.InstanceOf<NotifyingObject>());
        }

        [Test]
        public void ViewModel_ByDefault_DerivesIDataErrorInfo()
        {
            var viewModel = GetViewModel();

            Assert.That(viewModel, Is.InstanceOf<IDataErrorInfo>());
        }

        [Test]
        public void Error_InvokingGetter_Throws()
        {
            var viewModel = GetViewModel();

            Assert.That(() => viewModel.Error, Throws.TypeOf<NotImplementedException>());
        }


        #region Factory

        private ViewModel GetViewModel()
        {
            return new FakeViewModel();
        }

        class FakeViewModel : ViewModel
        { }

        #endregion
    }
}
