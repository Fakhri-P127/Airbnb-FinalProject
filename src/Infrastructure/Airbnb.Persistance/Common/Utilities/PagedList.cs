using Airbnb.Application.Common.Interfaces;
using Airbnb.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Common.Utilities
{
    /// <summary>
    /// Bunu kodumda ishletmeye vaxtim chatmadi((. Yazandan sonra chatdi ki, her yerde bir bir deyishmeliyem 
    /// ve ona vaxtim chatmayacaq. 
    /// Plan burda yaranan pagecount,totalpage,currenpage ve s. deyerlerini headere yazdirmaq idi.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>,IPagedList<T> where T : BaseEntity
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public async static Task<IPagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = await source.CountAsync();
            List<T> items = await source.OrderByDescending(x=>x.CreatedAt).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
