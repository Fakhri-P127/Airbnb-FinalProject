using Airbnb.Application.Common.Interfaces.Repositories;
using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.Base;
using Airbnb.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
           
            IQueryable<T> query = expression is not null ?
                _dbSet.Where(expression) : _dbSet.AsQueryable();
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id, Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> query = expression is not null ?
                 _dbSet.Where(expression) : _dbSet.AsQueryable();
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
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

    }
}
