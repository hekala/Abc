using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public sealed class Measure: Entity<MeasureData>//vedada andmeid andmebaasi ja kasutajaliidese vahel, kui vahegi saab siis klass sealed!, ei saa parida 
    {
        public Measure(): this(null){ }
        public Measure(MeasureData data) : base(data) { }
    }    
}
