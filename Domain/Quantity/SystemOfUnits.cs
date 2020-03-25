using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public sealed class SystemOfUnits: Entity<SystemOfUnitsData>//vedada andmeid andmebaasi ja kasutajaliidese vahel, kui vahegi saab siis klass sealed!, ei saa parida 
    {
        public SystemOfUnits(): this(null){ }
        public SystemOfUnits(SystemOfUnitsData data) : base(data) { }

    }
}
