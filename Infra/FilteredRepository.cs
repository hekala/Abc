using System.Linq;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class FilteredRepository<TDomain, TData>: SortedRepository<TDomain, TData>, ISearching
        where TData: PeriodData, new()
        where TDomain: Entity<TData>, new()
    {
        public string SearchString { get; set; }

        protected FilteredRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }
        protected internal override IQueryable<TData> createSqlQuery() //overrideib baserep meetodi, nagu sortedRep!
        {
            var query = base.createSqlQuery();
            query = addFiltering(query);

            return query;
        }

        protected internal virtual IQueryable<TData> addFiltering(IQueryable<TData> query)
        {
            return query;
        }
    }
}