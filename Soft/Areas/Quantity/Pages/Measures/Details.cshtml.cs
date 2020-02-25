using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facade.Quantity;
using Pages;
using Abc.Domain.Quantity;

namespace Soft.Areas.Quantity.Pages.Measures
{
    public class DetailsModel : MeasuresPage
    {
        public DetailsModel(IMeasuresRepository r) : base(r) { }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            Item = MeasureViewFactory.Create(await data.Get(id));

            if (Item == null) return NotFound();          

            return Page();
        }
    }
}
