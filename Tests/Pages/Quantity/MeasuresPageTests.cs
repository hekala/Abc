using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages;
using Abc.Pages.Quantity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantity
{
    [TestClass]
    public class MeasuresPageTests : AbstractClassTests
        <MeasuresPage, CommonPage<IMeasuresRepository, Measure, MeasureView, MeasureData>>
    {
        private class testClass : MeasuresPage
        {
            internal testClass(IMeasuresRepository r, IMeasureTermsRepository t) : base(r, t) { }
        }

        private class testRepository : baseTestRepositoryForUniqueEntity<Measure, MeasureData>, IMeasuresRepository { } //fake repo klass
        private class termRepository :  baseTestRepositoryForPeriodEntity<MeasureTerm, MeasureTermData>,
            IMeasureTermsRepository {
            protected override bool isThis(MeasureTerm entity, string id)
            {
                return true;
            }

            protected override string getId(MeasureTerm entity)
            {
                return string.Empty;
            }
        }
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize(); //kasutab inmemorydb extensionit, mis on malus!
            var r = new testRepository();
            var t = new termRepository();
            obj = new testClass(r, t); //annan repository katte
        }

        [TestMethod] public void ItemIdTest()
        {
            var item = GetRandom.Object<MeasureView>();
            obj.Item = item; //on objekt, millel on item
            Assert.AreEqual(item.Id, obj.ItemId);
            obj.Item = null;
            Assert.AreEqual(string.Empty, obj.ItemId);
        }

        [TestMethod] 
        public void PageTitleTest() => Assert.AreEqual("Measures", obj.PageTitle);
        
        [TestMethod] 
        public void PageUrlTest() => Assert.AreEqual("/Quantity/Measures", obj.PageUrl);

        [TestMethod] public void ToObjectTest()
        {
            var view = GetRandom.Object<MeasureView>();
            var o = obj.toObject(view);
            testArePropertyValuesEqual(view, o.Data);
        }
        [TestMethod] public void ToViewTest()
        {
            var data = GetRandom.Object<MeasureData>();
            var view = obj.toView(new Measure(data));
            testArePropertyValuesEqual(view, data);
        }
        [TestMethod] public void LoadDetailsTest()
        {
            var v = GetRandom.Object<MeasureView>();
           obj.LoadDetails(v);
           Assert.IsNotNull(obj.Terms);
        }
        [TestMethod] public void TermsTest()
        {
            isReadOnlyProperty(obj, nameof(obj.Terms), obj.Terms);
        }

    }
}
