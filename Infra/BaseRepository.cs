using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class BaseRepository<TDomain, TData> : ICrudMethods<TDomain> 
        where TData: PeriodData, new()
        where TDomain: Entity<TData>, new()
    {
        protected internal DbContext db;
        protected internal DbSet<TData> dbSet;

        protected BaseRepository(DbContext c, DbSet<TData> s)
        {
            db = c;
            dbSet = s;
        }
       
        public virtual async Task<List<TDomain>> Get()
        {
            var query = createSqlQuery(); //teeb sql paringu
            var set = await runSqlQueryAsync(query); //jooksuta seda asunkroonselt, kusib andmebaasist andmed

            return toDomainObjectsList(set); //vii listi valdkonnaobjektid
        }

        internal List<TDomain> toDomainObjectsList(List<TData> set) => set.Select(tDomainObject).ToList();

        protected internal abstract TDomain tDomainObject(TData periodData);

        internal async Task<List<TData>>runSqlQueryAsync(IQueryable<TData> query) => await query.AsNoTracking().ToListAsync();
        
        protected internal virtual IQueryable<TData> createSqlQuery() //virtual, peab laskma jargm ule kirjutada!
        {
            var query = from s in dbSet select s; //tehakse sql paring

            return query;
        }

        public async Task<TDomain> Get(string id)
        {
            if (id is null) return new TDomain();

            var d = await getData(id);

            var obj = new TDomain{Data = d};

            return obj;
        }

        protected abstract Task<TData> getData(string id);
      
        public async Task Add(TDomain obj)
        {
            if (obj?.Data is null) return;
            dbSet.Add(obj.Data);
            await db.SaveChangesAsync();
        }
        public async Task Delete(string id)
        {           
            var d = await dbSet.FindAsync(id);

            if (d is null) return; 
            
            dbSet.Remove(d);
            await db.SaveChangesAsync();          
        }

        public async Task Update(TDomain obj)
        {
            db.Attach(obj.Data).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MeasureViewExists(MeasureView.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }
        }
    }
}