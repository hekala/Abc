using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using System;
using System.Linq;

namespace Abc.Infra.Quantity
{
    public class MeasureRepository : UniqueEntityRepository<Measure, MeasureData>, IMeasureRepository
    {
        public MeasureRepository(QuantityDbContext c) : base(c, c.Measures) { }
        //annab quantitydb cont katte!

        protected internal override Measure tDomainObject(MeasureData d) => new Measure(d); //annad saadud data d katte!

    }
}
