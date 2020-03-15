using Abc.Domain.Quantity;
using System;
using Abc.Aids;
using Abc.Data.Quantity;

namespace Abc.Facade.Quantity
{
    public static class MeasureViewFactory //static pole paritav!
    {
        public static Measure Create(MeasureView v)
        {
            var o = new Measure();
            Copy.Members(v, o.Data);
            
            return o; 
        }
        public static MeasureView Create(Measure o)
        {
            var v = new MeasureView();
            Copy.Members(o.Data, v);

            return v;
        }
    }
}
