using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Tests.Data.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantity
{
    [TestClass]
    public class MeasureViewFactoryTests: BaseTests
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(MeasureViewFactory); //testi seda klassi
        }

        [TestMethod]
        public void CreateTest() { }
       
        [TestMethod]
        public void CreateObjectTest()
        {
            var view = GetRandom.Object<MeasureView>();
            var data = MeasureViewFactory.Create(view).Data; //annan view katte talle

            testArePropertyValuesEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest()
        {
            var data = GetRandom.Object<MeasureData>();
            var view = MeasureViewFactory.Create(new Measure(data)); //annan view katte talle

            testArePropertyValuesEqual(view, data);
        }
    }
}
