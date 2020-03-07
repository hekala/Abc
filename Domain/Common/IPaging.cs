namespace Abc.Domain.Common
{
    public interface IPaging
    {
        int PageIndex { get; set; }
        int PageSize { get; set; } //keegi utleb ette mitu kirjet uhele lk laheb 
        int TotalPages { get; } //ise arvutab palju lk vaja
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
    }
}