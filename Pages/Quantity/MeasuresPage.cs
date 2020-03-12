using System;
using Abc.Domain.Quantity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Facade.Quantity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
        public string SearchString { get; set; }

        public bool HasPreviousPage => data.HasPreviousPage; //laheb kusib andmebaasi kaest
        public bool HasNextPage => data.HasNextPage;

        public int PageIndex
        {
            get=> data.PageIndex; //kui keegi kusib siis saab andmebaasilt
            set=> data.PageIndex = value; //kui keegi muudab siis vaartus laheb andmebaasile
        }

        public int TotalPages => data.TotalPages; 

        protected internal async Task<bool> addObject() //andmebaasile ligiminek peab olema asunkroonne!
        {
            //TODO see viga lahendada!
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            try
            {
                if (!ModelState.IsValid) return false;
                await data.Add(MeasureViewFactory.Create(Item));

            }
            catch
            {
                return false;
            }
            return true;
        }

        protected internal async Task updateObject()
        {
            //TODO see viga lahendada!
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.

            await data.Update(MeasureViewFactory.Create(Item));
        }

        protected internal async Task getObject(string id)
        {
            var o = await data.Get(id);
            Item = MeasureViewFactory.Create(o);
        }

        protected internal async Task deleteObject(string id)
        {
            await data.Delete(id); //laheb kustutab andmebaasist
        }
        public string GetSortString(Expression<Func<MeasureData, object>> e, string page)
        {
            
            var name = GetMember.Name(e); //aidsis tehtud meetod, annab expressioni katte
            string sortOrder;
            if (string.IsNullOrEmpty(CurrentSort)) sortOrder = name;
            else if (!CurrentSort.StartsWith(name)) sortOrder = name; //kui currentsort ei alga nimega, siis name
            else if (CurrentSort.EndsWith("_desc")) sortOrder = name;
            else sortOrder = name + "_desc";
            return $"{page}?sortOrder={sortOrder}&currentFilter={CurrentFilter}";
        }

        protected internal async Task getList(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder; //kui on tuhi siis sorteerib nime jargi
            CurrentSort = sortOrder;
            
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            data.SortOrder = sortOrder;
            SearchString = CurrentFilter;
            data.SearchString = searchString;

            PageIndex = pageIndex ?? 1;
            var l = await data.Get();
            Items = new List<MeasureView>();
            foreach (var e in l) Items.Add(MeasureViewFactory.Create(e));
        }
    }
}
