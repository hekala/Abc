﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
{
    public class DetailsModel : MeasuresPage
    {
        public DetailsModel(IMeasureRepository r) : base(r) { }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            await getObject(id);       

            return Page();
        }
    }
}
