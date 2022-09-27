using System.Linq.Expressions;

namespace Airbnb.Application.Common.Interfaces.Repositories.Common
{
    public interface IGenericRepository<T>
    {
        Task<List<T>>  GetAllAsync(Expression<Func<T, bool>> expression, bool tracked = false, params string[] includes);
        Task<T> GetByIdAsync(Guid id, Expression<Func<T, bool>> expression, bool tracked = false, params string[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracked = false, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity, bool state = true);
        Task DeleteAsync(T entity);
    }
}
