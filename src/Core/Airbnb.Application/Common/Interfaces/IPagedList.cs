namespace Airbnb.Application.Common.Interfaces
{
    public interface IPagedList<T>:IList<T>
    {
        public int CurrentPage { get;}
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        //Task<IPagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize);
     
    }
}
