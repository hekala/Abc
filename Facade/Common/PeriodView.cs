using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Common
{
    public abstract class PeriodView
    {
        [DataType(DataType.Date)]
        [DisplayName("Valid from")]
        public DateTime? ValidFrom{ get; set; } //annab ainult nulli muidu
        [DataType(DataType.Date)] //tuleb lisada 
        [DisplayName("Valid to")]
        public DateTime? ValidTo { get; set; }
    }
}