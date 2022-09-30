using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Domain.Entities.Base;
using Airbnb.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airbnb.Persistance.Common.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AirbnbDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AirbnbDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            BaseQueryStringParameters parameters, bool tracked = false, params string[] includes)
        {
            IQueryable<T> query = expression is not null ?
                _dbSet.Where(expression) : _dbSet.AsQueryable();
            query = tracked is false ?
                 query.AsNoTrackingWithIdentityResolution() : query;//AsSplitQuery elemek olar
            query = SetIncludes(query, includes);

            return parameters is not null ? await query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToListAsync() : await query.ToListAsync();
            //return await PagedList<T>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, Expression<Func<T, bool>> expression, bool tracked = false, params string[] includes)
        {
            IQueryable<T> query = expression is not null ?
                 _dbSet.Where(expression) : _dbSet.AsQueryable();
            query = SetIncludes(query, includes);
            return tracked is false ? await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
                : await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        public virtual T GetById(Guid id, Expression<Func<T, bool>> expression, bool tracked = false, params string[] includes)
        {
            IQueryable<T> query = expression is not null ?
                 _dbSet.Where(expression) : _dbSet.AsQueryable();
            query = SetIncludes(query, includes);
            return tracked is false ? query.AsNoTracking().FirstOrDefault(x => x.Id == id)
                : query.FirstOrDefault(x => x.Id == id);
        }
        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracked = false, params string[] includes)
        {
            IQueryable<T> query = expression is not null ?
                _dbSet.Where(expression) : _dbSet.AsQueryable();
            query = SetIncludes(query, includes);
            return tracked is false ? await query.AsNoTracking().FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity, bool state = true)
        {
            if (state)
            {
                _context.Entry(entity).State = EntityState.Unchanged;
            }
            else
            {
                _dbSet.Attach(entity);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        private static IQueryable<T> SetIncludes(IQueryable<T> query, string[] includes)
        {
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}
