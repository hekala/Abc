using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;

namespace Abc.Pages.Quantity
{
    public class UnitFactorsPage: CommonPage<IUnitFactorsRepository, UnitFactor, UnitFactorView, UnitFactorData>
    {
        protected internal UnitFactorsPage(IUnitFactorsRepository r): base(r)
        {
            PageTitle = "Unit Factors";
        }
        public override string ItemId 
        {
            get
            {
                if (Item is null) return string.Empty;
                return $"{Item.SystemOfUnitsId}.{Item.UnitId}";
            }
        }
        protected internal override string getPageUrl() => "/Quantity/UnitFactors";
      
        protected internal override UnitFactor toObject(UnitFactorView view)
        {
            return UnitFactorViewFactory.Create(view);
        }

        protected internal override UnitFactorView toView(UnitFactor obj)
        {
            return UnitFactorViewFactory.Create(obj);
        }
    }
}
