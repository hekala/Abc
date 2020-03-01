using Abc.Data.Common;
using Abc.Data.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class SealedClassTest<TClass, TBaseClass>: ClassTest<TClass, TBaseClass> where TClass : new()
    {
        [TestMethod]
        public void IsSealed()
        {
            Assert.IsTrue(type.IsSealed);
        }
    }
}