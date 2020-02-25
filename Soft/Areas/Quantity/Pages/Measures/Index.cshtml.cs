using System.Collections.Generic;
using System.Threading.Tasks;
using Pages;
using Abc.Domain.Quantity;
using Facade.Quantity;

namespace Soft.Areas.Quantity.Pages.Measures
{
    public class IndexModel : MeasuresPage
    {
        public IndexModel(IMeasuresRepository r) : base(r) { }
       
        public async Task OnGetAsync()
        {
            var l = await data.Get();
            Items = new List<MeasureView>();
            foreach (var e in l)
            {
                Items.Add(MeasureViewFactory.Create(e));
            }            
        }
    }
}
