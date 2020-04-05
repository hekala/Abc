using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Infra.Quantity {

    public sealed class SystemsOfUnitsRepository : UniqueEntityRepository<SystemOfUnits, SystemOfUnitsData>,
        ISystemsOfUnitsRepository {

        public SystemsOfUnitsRepository() : this(null) { }
        public SystemsOfUnitsRepository(QuantityDbContext c) : base(c, c?.SystemsOfUnits) { }

        protected internal override SystemOfUnits tDomainObject(SystemOfUnitsData d) => new SystemOfUnits(d);
    }
}

