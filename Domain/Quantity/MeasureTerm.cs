using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public sealed class MeasureTerm: Entity<MeasureTermData>//vedada andmeid andmebaasi ja kasutajaliidese vahel, kui vahegi saab siis klass sealed!, ei saa parida 
    {
        public MeasureTerm(): this(null){ }
        public MeasureTerm(MeasureTermData data) : base(data) { }
    }
}
