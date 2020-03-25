using Abc.Data.Common;

namespace Abc.Data.Quantity
{
    public abstract class CommonTermData: PeriodData
    {
        public string MasterId { get; set; } //nt see masterid hoiab kiiruse id
        public string TermId { get; set; } 
        public int Power { get; set; } //aste, kas uhik mida selle termiga naidatakse on astmes 1 jne.. 
    }
}
