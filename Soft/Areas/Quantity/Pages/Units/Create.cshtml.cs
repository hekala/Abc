﻿using System.Collections.Generic;
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

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync() //viisin osa koodi measurepagei (parem koht)
        {
            if (!await addObject()) return Page();
            return RedirectToPage("./Index");
        }
    }
}
