using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public class Measure: Entity<MeasureData>//vedada andmeid andmebaasi ja kasutajaliidese vahel
    {
        public Measure(): this(null){ }
        public Measure(MeasureData data) : base(data)
        {
        }

    }    
}
