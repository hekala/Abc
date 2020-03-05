using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class SortedRepository<TDomain, TData>: BaseRepository<TDomain, TData>, ISorting
        where TData: PeriodData, new()
        where TDomain: Entity<TData>, new()
    {
        public string SortOrder { get; set; }

        protected SortedRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }
    }
}