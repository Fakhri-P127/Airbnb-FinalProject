using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces.Repositories.Common
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<T> GetByIdAsync(Guid id, Expression<Func<T, bool>> expression, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity, bool state = true);
        Task DeleteAsync(T entity);
    }
}
