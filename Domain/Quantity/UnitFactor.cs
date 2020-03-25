using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public sealed class UnitFactor: Entity<UnitFactorData>//vedada andmeid andmebaasi ja kasutajaliidese vahel, kui vahegi saab siis klass sealed!, ei saa parida 
    {
        public UnitFactor(): this(null){ }
        public UnitFactor(UnitFactorData data) : base(data) { }
    }
}
