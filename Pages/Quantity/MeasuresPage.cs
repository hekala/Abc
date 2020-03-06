﻿using Abc.Domain.Quantity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Abc.Facade.Quantity;

namespace Abc.Pages.Quantity
{
    public abstract class MeasuresPage : PageModel
    {
        protected internal readonly IMeasuresRepository data;

        protected internal MeasuresPage(IMeasuresRepository r)
        {
            data = r;
            PageTitle = "Measures";
        }
        

        [BindProperty]
        public MeasureView Item { get; set; }    
        public IList<MeasureView> Items { get; set; }

        public string ItemId => Item.Id;
        public string PageTitle { get; set; } 
        public string PageSubTitle { get; set; }
        public string CurrentSort{ get; set; } = "Current Sort";
        public string CurrentFilter{ get; set; } = "Current Filter";
        public int PageIndex { get; set; } = 3;
        public int TotalPages { get; set; } = 10;


    }
}
