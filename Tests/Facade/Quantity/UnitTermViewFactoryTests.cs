using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantity
{
    [TestClass]
    public class UnitTermViewFactoryTests: BaseTests
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(UnitTermViewFactory); //testi seda klassi
        }

        [TestMethod]
        public void CreateTest() { }
       
        [TestMethod]
        public void CreateObjectTest()
        {
            var view = GetRandom.Object<UnitTermView>();
            var data = UnitTermViewFactory.Create(view).Data; //annan view katte talle

            testArePropertyValuesEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest()
        {
            var data = GetRandom.Object<UnitTermData>();
            var view = UnitTermViewFactory.Create(new UnitTerm(data)); //annan view katte talle

            testArePropertyValuesEqual(view, data);
        }       
    }
    
}
