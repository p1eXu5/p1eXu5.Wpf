using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

namespace p1eXu5.Wpf.MvvmBaseLibrary.Tests.FunctionalTests
{
    [TestFixture]
    public class NotifyingObjectTests
    {
        [Test]
        public void NotifyingObject_IsAbstract()
        {
            Assert.That(typeof(NotifyingObject).IsAbstract);
        }

        [Test]
        public void NotifyingObject_SetProperty_CanRisePropertyChangedEvent()
        {
            // Arrange:
            var notifyingObject = GetNotifyingObject();
            var res = false;
            notifyingObject.PropertyChanged += (s, e) => { res = true; };

            // Action:
            ((FakeNotifyingObject) notifyingObject).TestProperty = "";

            // Assert:
            Assert.That(true == res);
        }

        [Test]
        public void NotifyingObject_RisesPropertyChanged_PassesPropertyNameByDefault()
        {
            // Arrange:
            var notifyingObject = GetNotifyingObject();
            var property = notifyingObject.GetType().GetProperties().First();

            string res = null;
            notifyingObject.PropertyChanged += (s, e) => { res = e.PropertyName; };

            // Action:
            property.SetValue(notifyingObject, "");

            // Assert:
            Assert.That(property.Name == res);
        }

        #region Factory

        private INotifyPropertyChanged GetNotifyingObject()
        {
            return new FakeNotifyingObject();
        }

        class FakeNotifyingObject : NotifyingObject
        {
            private string _testProperty;

            public string TestProperty
            {
                get => _testProperty;
                set {
                    _testProperty = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}
