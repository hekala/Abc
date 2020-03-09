using System;
using System.Linq;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class PaginatedRepository<TDomain, TData>: FilteredRepository<TDomain, TData>, IPaging
        where TData: PeriodData, new()
        where TDomain: Entity<TData>, new()
    {
        public int PageIndex { get; set; }
        public int TotalPages => getTotalPages(PageSize);

        public bool HasNextPage => PageIndex < TotalPages; //lk indeks peab olema vaiksem kui lk arv, siis on veel jargm lk olemas
        public bool HasPreviousPage => PageIndex > 1; //siis on eelm lk olemas
        public int PageSize { get; set; } = 5;

        protected PaginatedRepository(DbContext c, DbSet<TData> s) : base(c, s) { }

        internal int getTotalPages(in int pageSize)
        {
            var count = getItemsCount(); //palju on kirjeid
            var pages = countTotalPages(count, pageSize);

            return pages;
        }

        internal int countTotalPages(int count, in int pageSize) //internal on nagu private, aga saab testida. keegi valjast ligi ei saa
        {
            return (int)Math.Ceiling(count / (double)pageSize);
        }

        internal int getItemsCount() => base.createSqlQuery().CountAsync().Result; //result ehk saame asunkr meetodit kutsuda valja sunkr meetodis
        
        protected internal override IQueryable<TData> createSqlQuery() => addSkipAndTake(base.createSqlQuery()); //lisab skipi (see oli paginatedlist kirjas)
    
        private IQueryable<TData> addSkipAndTake(IQueryable<TData> query) => query.
            Skip((PageIndex - 1) * PageSize)
                .Take(PageSize);
    }
}