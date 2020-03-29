using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantity
{
    [TestClass]
    public class MeasureTermViewFactoryTests: BaseTests
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(MeasureTermViewFactory); //testi seda klassi
        }

        [TestMethod]
        public void CreateTest() { }
       
        [TestMethod]
        public void CreateObjectTest()
        {
            var view = GetRandom.Object<MeasureTermView>();
            var data = MeasureTermViewFactory.Create(view).Data; //annan view katte talle

            testArePropertyValuesEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest()
        {
            var data = GetRandom.Object<MeasureTermData>();
            var view = MeasureTermViewFactory.Create(new MeasureTerm(data)); //annan view katte talle

            testArePropertyValuesEqual(view, data);
        }       
    }
}
