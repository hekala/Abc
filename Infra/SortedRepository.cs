using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public string DescendingString => "_desc";

        protected SortedRepository(DbContext c, DbSet<TData> s) : base(c, s) { }

        protected internal override IQueryable<TData> createSqlQuery() //kirjutan ule baserep meetodi!
        {
            var query = base.createSqlQuery(); //votab baasist query
            query = addSorting(query); //lisab sorteerimise queryle

            return query; //tagastab query
        }

        protected internal IQueryable<TData> addSorting(IQueryable<TData> query) //kirjutab sql lause milles on sorteerimine sees
        {
            var expression = createExpression();

            var r = expression is null ? query : addOrderBy(query, expression);

            return r;
        }

        internal Expression<Func<TData, object>> createExpression()
        {
            var property = findProperty();

            if (property is null) return null;
            return lambdaExpression(property);
        }

        internal Expression<Func<TData, object>> lambdaExpression(PropertyInfo p)
        { 
            var param = Expression.Parameter(typeof(TData), "x"); //millisest tuubist hakkad lambdaexp tegema
            var property = Expression.Property(param, p); //sellest paramaeetrist see propertyinfo (s => s.ValidFrom)
            var body = Expression.Convert(property, typeof(object)); //converti kogu property mida sa saad tuubiks
            return Expression.Lambda<Func<TData, object>>(body, param); //kogu body pane niimoodi kaima et ta on lambdaexpression
        }

        internal PropertyInfo findProperty()
        {
            var name = getName();
            return typeof(TData).GetProperty(name);
        }

        internal string getName() //votab sortorderist maha koik caracterid, mis hakkavad al descendingstringist nt kui id.desc siis ainult id
        {
            if (string.IsNullOrEmpty(SortOrder)) return string.Empty;
            var idx = SortOrder.IndexOf(DescendingString, StringComparison.Ordinal);
            
            return idx > 0 ? SortOrder.Remove(idx) : SortOrder; //kui index suurem kui 0, siis votab koik al indeksist ara 
        }

        internal IQueryable<TData> addOrderBy(IQueryable<TData> query, Expression<Func<TData, object>> e)
        {
            if (query is null) return null;
            if (e is null) return query; //kui e on null, aga data ei ole enam, siis annan data tagasi
            try
            {
                return isDescending() ? query.OrderByDescending(e) : query.OrderBy(e); //kui data ja e molemad olemas
            }
            catch
            {
                return query;
            }
        }

        internal bool isDescending() => !string.IsNullOrEmpty(SortOrder) && SortOrder.EndsWith(DescendingString); //maaratud uleval
        //kontrollib et pole null
    }
}