using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;
//TODO
//case "ValidFrom":
//measures = measures.OrderBy(s => s.ValidFrom);
//break;
//case "ValidFrom_desc":
//measures = measures.OrderByDescending(s => s.ValidFrom);
//break;

namespace Abc.Infra
{
    public abstract class SortedRepository<TDomain, TData>: BaseRepository<TDomain, TData>, ISorting
        where TData: PeriodData, new()
        where TDomain: Entity<TData>, new()
    {
        public string SortOrder { get; set; }
        public string DescendingString => "_desc";

        protected SortedRepository(DbContext c, DbSet<TData> s) : base(c, s) { }
        protected internal IQueryable<TData> setSorting(IQueryable<TData> data) //kirjutab sql lause milles on sorteerimine sees
        {
            var expression = createExpression();

            var r = expression is null ? data : setOrderBy(data, expression);

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
            var param = Expression.Parameter(typeof(TData)); //millisest tuubist hakkad lambdaexp tegema
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
            if (idx >= 0) return SortOrder.Remove(idx);

            return SortOrder;
        }

        internal IQueryable<TData> setOrderBy(IQueryable<TData> data, Expression<Func<TData, object>> e)
        {
            if (data is null) return null;
            if (e is null) return data; //kui e on null, aga data ei ole enam, siis annan data tagasi
            try
            {
                return isDescending() ? data.OrderByDescending(e) : data.OrderBy(e); //kui data ja e molemad olemas
            }
            catch
            {
                return data;
            }
        }

        internal bool isDescending() => !string.IsNullOrEmpty(SortOrder) && SortOrder.EndsWith(DescendingString); //maaratud uleval
        //kontrollib et pole null
    }
}