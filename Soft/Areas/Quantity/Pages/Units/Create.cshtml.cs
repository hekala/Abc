using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Soft.Areas.Quantity.Pages.Units
{
    public class CreateModel : UnitsPage
    {
        public CreateModel(IUnitsRepository r, IMeasuresRepository m) : base(r, m) { }

        public IActionResult OnGet(string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string fixedFilter, string fixedValue) //viisin osa koodi measurepagei (parem koht)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            if (!await addObject()) return Page();
            return Redirect($"/Quantity/Units/Index?fixedFilter={FixedFilter}&fixedValue={FixedValue}");
        }
    }
}
