using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra
{
    [TestClass]
    public class SortedRepositoryTests : AbstractClassTest<SortedRepository<Measure, MeasureData>,
        BaseRepository<Measure, MeasureData>>
    {
        private class testClass : SortedRepository<Measure, MeasureData>
        {
            public testClass(DbContext c, DbSet<MeasureData> s) : base(c, s)
            {
            }

            protected override async Task<MeasureData> getData(string id)
            {
                await Task.CompletedTask;
                return new MeasureData();
            }
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize(); //kasutab inmemorydb extensionit, mis on malus!
            var options = new DbContextOptionsBuilder<QuantityDbContext>().UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDbContext(options);
            obj = new testClass(c, c.Measures);
        }

        [TestMethod]
        public void SortOrderTest()
        {
            isNullableProperty(() => obj.SortOrder, x => obj.SortOrder=x);
        }

        [TestMethod]
        public void DescendingStringTest()
        {
            var propertyName = GetMember.Name<testClass>(x => x.DescendingString);
            isReadOnlyProperty(obj, propertyName, "_desc"); //eraldi meetod 
        }

        [TestMethod]
        public void SetSortingTest() //sarnane setorderby testiga, aga funktsiooni asemel annad ette stringi
        {
            void test(IQueryable<MeasureData> d, string sortOrder)
            {
                obj.SortOrder = sortOrder + obj.DescendingString;
                var set = obj.setSorting(d);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"Abc.Data.Quantity.MeasureData]).OrderByDescending(Param_0 => Convert(Param_0.{sortOrder}, Object))")); //laheb measuredatat saama ja laheb seda orderbydesc jargi
                obj.SortOrder = sortOrder;
                set = obj.setSorting(d);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString().Contains($"Abc.Data.Quantity.MeasureData]).OrderBy(Param_0 => Convert(Param_0.{sortOrder}, Object))"));
            }
            Assert.IsNull(obj.setSorting(null)); 
            IQueryable<MeasureData> data = obj.dbSet;
            obj.SortOrder = null;
            Assert.AreEqual(data, obj.setSorting(data));
            test(data, GetMember.Name<MeasureData>(x => x.Id));
            test(data, GetMember.Name<MeasureData>(x => x.Code));
            test(data, GetMember.Name<MeasureData>(x => x.Name));
            test(data, GetMember.Name<MeasureData>(x => x.Definition));
                        test(data, GetMember.Name<MeasureData>(x => x.ValidFrom));
                            test(data, GetMember.Name<MeasureData>(x => x.ValidTo));
        }

        [TestMethod]
        public void CreateExpressionTest()
        {
            string s;
            testCreateExpression(GetMember.Name<MeasureData>(x => x.ValidFrom));
            testCreateExpression(GetMember.Name<MeasureData>(x => x.ValidTo));
            testCreateExpression(GetMember.Name<MeasureData>(x => x.Id));
            testCreateExpression(GetMember.Name<MeasureData>(x => x.Name));
            testCreateExpression(GetMember.Name<MeasureData>(x => x.Code));
            testCreateExpression(GetMember.Name<MeasureData>(x => x.Definition));
            testCreateExpression(s = GetMember.Name<MeasureData>(x => x.ValidFrom), s+obj.DescendingString);
            testCreateExpression(s = GetMember.Name<MeasureData>(x => x.ValidTo), s+obj.DescendingString);
            testCreateExpression(s = GetMember.Name<MeasureData>(x => x.Id), s+obj.DescendingString);
            testCreateExpression(s = GetMember.Name<MeasureData>(x => x.Name), s+obj.DescendingString);
            testCreateExpression(s = GetMember.Name<MeasureData>(x => x.Code), s+obj.DescendingString);
            testCreateExpression(s = GetMember.Name<MeasureData>(x => x.Definition), s+obj.DescendingString);
            testNullExpression(GetRandom.String());
            testNullExpression(string.Empty);
            testNullExpression(null);
        }

        private void testNullExpression(string name)
        { 
            obj.SortOrder = name;
            var lambda = obj.createExpression();
            Assert.IsNull(lambda); 
        }

        private void testCreateExpression(string expected, string name = null)
        {
            name ??= expected; //kui nimi on null siis vota expected
            obj.SortOrder = name;
            var lambda = obj.createExpression();
            Assert.IsNotNull(lambda); 
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<MeasureData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(expected));
        }

        [TestMethod] public void LambdaExpressionTest()
        {
            var name = GetMember.Name<MeasureData>(x => x.ValidFrom); //mingi property nime vaja teada
            var property = typeof(MeasureData).GetProperty(name); //otsib nime jargi property
            var lambda = obj.lambdaExpression(property); //annab property katte ja saab lambdaexpr
            Assert.IsNotNull(lambda); //ei tohi olla null
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<MeasureData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(name));
        }

        [TestMethod]
        public void FindPropertyTest()
        {
            string s;

            void test(PropertyInfo expected, string sortOrder)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.findProperty());
            }

            test(null, GetRandom.String());
            test(null, null);
            test(null, string.Empty);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Name)), s);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidFrom)), s);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidTo)), s);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Definition)), s);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Code)), s);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Id)), s);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Name)), s+obj.DescendingString);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidFrom)), s+obj.DescendingString);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidTo)), s+obj.DescendingString);
            test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Definition)), s+obj.DescendingString);
        }

        [TestMethod] public void GetNameTest()
        { 
            string s;

            void test(string expected, string sortOrder)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.getName());
            }

            test(s = GetRandom.String(), s);
            test(s = GetRandom.String(), s + obj.DescendingString);
            test(string.Empty, string.Empty);
            test(string.Empty, null);
        }

        [TestMethod]
        public void SetOrderByTest()   
        {
            void test(IQueryable<MeasureData> d, Expression<Func<MeasureData, object>> e, string expected)
            {
                obj.SortOrder = GetRandom.String() + obj.DescendingString;
                var set = obj.setOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"Abc.Data.Quantity.MeasureData]).OrderByDescending({expected})")); //laheb measuredatat saama ja laheb seda orderbydesc jargi
                obj.SortOrder = GetRandom.String();
                set = obj.setOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString().Contains($"Abc.Data.Quantity.MeasureData]).OrderBy({expected})"));
            }

            Assert.IsNull(obj.setOrderBy(null, null)); 
            IQueryable<MeasureData> data = obj.dbSet;
            Assert.AreEqual(data, obj.setOrderBy(data, null));
            test(data, x => x.Id, "x => x.Id");
            test(data, x => x.Code, "x => x.Code");
            test(data, x => x.Name, "x => x.Name");
            test(data, x => x.Definition, "x => x.Definition");
            test(data, x => x.ValidFrom, "x => Convert(x.ValidFrom, Object)");
            test(data, x => x.ValidTo, "x => Convert(x.ValidTo, Object)");
        }

        [TestMethod]
        public void IsDescendingTest()
        {
            void test(string sortOrder, bool expected)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.isDescending()); //NB! pead lisama assemblyinfo igale projektile, muidu isdescending ei leia!
            }
            test(GetRandom.String(), false);
            test(GetRandom.String() + obj.DescendingString, true); //annab descendingstringi ette ehk peaks tulema true
            test(string.Empty, false);
            test(null, false);
        }
    }
}
