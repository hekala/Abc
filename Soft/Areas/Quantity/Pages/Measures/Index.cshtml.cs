using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Facade.Quantity;
using Soft.Data;

namespace Soft.Areas.Quantity.Pages.Measures
{
    public class IndexModel : PageModel
    {
        private readonly Soft.Data.ApplicationDbContext _context;

        public IndexModel(Soft.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MeasureView> MeasureView { get;set; }

        public async Task OnGetAsync()
        {
            MeasureView = await _context.Measures.ToListAsync();
        }
    }
}
