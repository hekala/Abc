namespace Abc.Domain.Common
{
    public interface IRepository<T> : ICrudMethods<T>, ISorting, ISearching, IPaging
    {
    }
}