using NUnit.Framework;
using System;
using System.ComponentModel;
using p1eXu5.Wpf.Mvvm;

namespace p1eXu5.Wpf.Mvvm.Tests.UnitTests
{
    [TestFixture]
    public class ViewModelBaseUTests
    {
        [Test]
        public void ViewModel_IsAbstract()
        {
            Assert.That(typeof(ViewModelBase).IsAbstract);
        }

        [Test]
        public void ViewModel_ByDefault_DerivesNotifierObject()
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

        private ViewModelBase GetViewModel()
        {
            return new FakeViewModel();
        }

        class FakeViewModel : ViewModelBase
        { }

        #endregion
    }
}
