namespace Abc.Domain.Common
{
    public interface IFiltering
    {
        string SearchString { get; set; }
        string FixedFilter { get; set; } //annan konkreetse property nime mille jargi filtr
        string FixedValue { get; set; } //fikseeritud vali, mida fiks filter peab sisaldama
    }
}