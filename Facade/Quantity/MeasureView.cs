using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Facade.Quantity
{
    public class MeasureView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Definition { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid from")]
        public DateTime? ValidFrom{ get; set; } //annab ainult nulli muidu
        [DataType(DataType.Date)] //tuleb lisada 
        [DisplayName("Valid to")]
        public DateTime? ValidTo { get; set; }
    }
}
