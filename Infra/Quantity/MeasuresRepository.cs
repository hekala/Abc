using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using System;
using System.Linq;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository: UniqueEntityRepository<Measure, MeasureData>, IMeasuresRepository
    {
        public MeasuresRepository(QuantityDbContext c) : base(c, c.Measures) { } //annab quantitydb cont katte!
        
        protected internal override Measure tDomainObject(MeasureData d) => new Measure(d); //annad saadud data d katte!

       protected internal override IQueryable<MeasureData> addFiltering(IQueryable<MeasureData> set)
        {
            if (String.IsNullOrEmpty(SearchString)) return set;
            return set.Where(s => s.Name.Contains(SearchString)
                                  || s.Code.Contains(SearchString) 
                                  || s.Id.Contains(SearchString) 
                                  || s.Definition.Contains(SearchString) 
                                  || s.ValidFrom.ToString().Contains(SearchString) 
                                  || s.ValidTo.ToString().Contains(SearchString));
        }
    }
}
