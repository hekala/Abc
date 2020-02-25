using Abc.Data.Quantity;

namespace Abc.Domain.Quantity
{
    public class Measure: Entity<MeasureData>//vedada andmeid andmebaasi ja kasutajaliidese vahel
    {
        public Measure(MeasureData data): base(data)
        {

        }
    }    
}
