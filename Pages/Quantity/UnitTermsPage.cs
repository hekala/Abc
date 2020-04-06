using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;

namespace Abc.Pages.Quantity
{
    public class UnitTermsPage: CommonPage<IUnitTermsRepository, UnitTerm, UnitTermView, UnitTermData>
    {
        protected internal UnitTermsPage(IUnitTermsRepository r): base(r)
        {
            PageTitle = "Unit Terms";
        }
        public override string ItemId 
        {
            get
            {
                if (Item is null) return string.Empty;
                return $"{Item.MasterId}.{Item.TermId}";
            }
        }
        protected internal override string getPageUrl() => "/Quantity/UnitTerms";
      
        protected internal override UnitTerm toObject(UnitTermView view)
        {
            return UnitTermViewFactory.Create(view);
        }

        protected internal override UnitTermView toView(UnitTerm obj)
        {
            return UnitTermViewFactory.Create(obj);
        }
    }
}
