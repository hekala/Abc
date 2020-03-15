using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common
{
    [TestClass]
    public class EntityTests: AbstractClassTests<Entity<MeasureData>, object> //entity on domainis abstract!
    {
        private class testClass : Entity<MeasureData>
        {
            public testClass(MeasureData d = null): base(d) { }
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new testClass();
        }

        [TestMethod]
        public void DataTest()
        {
            var d = GetRandom.Object<MeasureData>(); //annad data testile ja pead prst selle saama!
            Assert.AreNotSame(d, obj.Data);
            obj = new testClass(d);
            Assert.AreSame(d, obj.Data);
        }

        
        [TestMethod]
        public void DataIsNullTest()
        {
            var d = GetRandom.Object<MeasureData>();
            Assert.IsNotNull(obj.Data); //kui midagi datale ei anna siis kas on 0
            obj.Data = d;
            Assert.AreSame(d, obj.Data);
        }

        [TestMethod]
        public void CanSetNullDataTest()
        {
            Assert.IsNotNull(obj.Data);
            obj.Data = null;
            Assert.IsNull(obj.Data); //nuud peab olema 0
        }
    }
}
