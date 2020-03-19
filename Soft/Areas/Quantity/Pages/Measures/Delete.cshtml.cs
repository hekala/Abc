using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages.Quantity;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
{
    public class DeleteModel : MeasuresPage
    {
        public DeleteModel(IMeasuresRepository r) : base(r) { }
        public async Task<IActionResult> OnGetAsync(string id, string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            await getObject(id);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            await deleteObject(id);

            return Redirect($"/Quantity/Measures/Index?fixedFilter={FixedFilter}&fixedValue={FixedValue}");
        }

    }
}
