using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages.Quantity;

namespace Abc.Soft.Areas.Quantity.Pages.SystemOfUnits
{
    public class DeleteModel : SystemsOfUnitsPage
    {
        public DeleteModel(ISystemsOfUnitsRepository r) : base(r) { }
        public async Task<IActionResult> OnGetAsync(string id, string fixedFilter, string fixedValue)
        {
            await getObject(id, fixedFilter, fixedValue);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string fixedFilter, string fixedValue)
        {
            await deleteObject(id, fixedFilter, fixedValue);

            return Redirect(IndexUrl);
        }

    }
}
