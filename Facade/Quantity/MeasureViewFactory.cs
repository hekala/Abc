using Abc.Domain.Quantity;
using Abc.Aids;
using Abc.Data.Quantity;

namespace Abc.Facade.Quantity
{
    public static class MeasureViewFactory //static pole paritav!
    {
        public static Measure Create(MeasureView v)
        {
            var d = new MeasureData();
            Copy.Members(v, d);
            
            return new Measure(d); 
        }
        public static MeasureView Create(Measure o)
        {
            var v = new MeasureView();
            if (!(o?.Data is null))
               Copy.Members(o.Data, v);
            return v;
        }
    }
}
