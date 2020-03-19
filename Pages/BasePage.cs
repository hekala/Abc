using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages
{
    public abstract class BasePage<TRepository, TDomain, TView, TData>: PageModel
    where TRepository: ICrudMethods<TDomain>, ISorting, IFiltering, IPaging
    {
        private TRepository data;

        protected internal BasePage(TRepository r)
        {
            data = r;
        }

        [BindProperty]
        public TView Item { get; set; }    
        public IList<TView> Items { get; set; }
        public abstract string ItemId { get; }


        public string PageTitle { get; set; }
        public string PageSubTitle => getPageSubtitle();

        protected internal virtual string getPageSubtitle()
        {
            return string.Empty;
        }

        public string FixedValue
        {
            get => data.FixedValue;
            set => data.FixedValue = value;
        }

        public string FixedFilter
        {
            get => data.FixedFilter;
            set => data.FixedFilter = value;
        }

        public string SortOrder
        {
            get => data.SortOrder;
            set => data.SortOrder = value;
        }

        public string SearchString
        {
            get => data.SearchString;
            set => data.SearchString = value;
        }

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
                await data.Add(toObject(Item));

            }
            catch
            {
                return false;
            }
            return true;
        }

        protected internal abstract TDomain toObject(TView view);

        protected internal async Task updateObject()
        {
            //TODO see viga lahendada!
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.

            await data.Update(toObject(Item));
        }

        protected internal async Task getObject(string id)
        {
            var o = await data.Get(id);
            Item = toView(o);
        }

        protected internal abstract TView toView(TDomain obj);
      
        protected internal async Task deleteObject(string id)
        {
            await data.Delete(id); //laheb kustutab andmebaasist
        }
        public string GetSortString(Expression<Func<TData, object>> e, string page)
        {
            
            var name = GetMember.Name(e); //aidsis tehtud meetod, annab expressioni katte
            string sortOrder;
            if (string.IsNullOrEmpty(SortOrder)) sortOrder = name;
            else if (!SortOrder.StartsWith(name)) sortOrder = name; //kui currentsort ei alga nimega, siis name
            else if (SortOrder.EndsWith("_desc")) sortOrder = name;
            else sortOrder = name + "_desc";

            return $"{page}?sortOrder={sortOrder}&currentFilter={SearchString}" //liidab stringid kokku
                   +$"&fixedFilter={FixedFilter}&fixedValue={FixedValue}";
        }

        protected internal async Task getList(string sortOrder, string currentFilter, string searchString, int? pageIndex, string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            SortOrder = sortOrder; //tuleb serveri poolt
            SearchString = getSearchString(currentFilter, searchString, ref pageIndex); //currentf saadetaksekogu aeg serverist kliendile uuesti
            PageIndex = pageIndex ?? 1;
            Items = await getList();
        }
        private string getSearchString(string currentFilter, string searchString, ref int? pageIndex)
        {
            if (searchString != null) { pageIndex = 1; }
            else { searchString = currentFilter; }

            return searchString;
        }

        internal async Task<List<TView>> getList()
        {
            var l = await data.Get();
            var list = new List<TView>();
            foreach (var e in l) list.Add(toView(e));

            return list;
        }
    }
}
