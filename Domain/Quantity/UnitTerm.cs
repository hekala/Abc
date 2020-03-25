using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public sealed class UnitTerm: Entity<UnitTermData>//vedada andmeid andmebaasi ja kasutajaliidese vahel, kui vahegi saab siis klass sealed!, ei saa parida 
    {
        public UnitTerm(): this(null){ }
        public UnitTerm(UnitTermData data) : base(data) { }
    }
}
