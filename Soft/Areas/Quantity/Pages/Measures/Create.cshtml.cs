using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Facade.Quantity;
using Soft.Data;

namespace Soft.Areas.Quantity.Pages.Measures
{
    public class CreateModel : PageModel
    {
        private readonly Soft.Data.ApplicationDbContext _context;

        public CreateModel(Soft.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MeasureView MeasureView { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Measures.Add(MeasureView);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
