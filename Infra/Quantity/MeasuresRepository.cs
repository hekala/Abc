using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Infra.Quantity
{
    public sealed class MeasuresRepository : UniqueEntityRepository<Measure, MeasureData>, IMeasuresRepository
    {
        public MeasuresRepository(QuantityDbContext c) : base(c, c.Measures) { }
        //annab quantitydb cont katte!

        protected internal override Measure tDomainObject(MeasureData d) => new Measure(d); //annad saadud data d katte!
    }
}
