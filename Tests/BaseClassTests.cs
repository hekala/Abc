using System;
using Abc.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests
{
    public abstract class BaseClassTests<TClass, TBaseClass>: BaseTests
    {
        protected TClass obj;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(TClass);
        }

        [TestMethod]
        public void IsInheritedTest()
        {
            Assert.AreEqual(typeof(TBaseClass), type.BaseType);
        }
        protected static void isNullableProperty<T>(Func<T> get, Action<T> set)
        {
            isProperty(get, set);
            set(default);
            Assert.IsNull(get());
        }
        protected static void isProperty<T>(Func<T> get, Action<T> set) 
        {
            var d = (T) GetRandom.Value(typeof(T)); //(T) teisenda Tks
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
        }
        protected static void isReadOnlyProperty(object o, string name, object expected)
        {
            var property = o.GetType().GetProperty(name); //objekti jargi saan tuubi, siis property nime
            Assert.IsNotNull(property); //prop ei tohi olla null
            Assert.IsFalse(property.CanWrite); //puudub setter
            Assert.IsTrue(property.CanRead); //tal on olemas getter
            var actual = property.GetValue(o);
            Assert.AreEqual(expected, actual);
        }

    }
}