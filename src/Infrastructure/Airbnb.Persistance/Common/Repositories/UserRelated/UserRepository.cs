﻿using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airbnb.Persistance.Common.Repositories.UserRelated
{
    public class UserRepository : IUserRepository
    {
        private readonly AirbnbDbContext _context;
        private readonly DbSet<AppUser> _dbSet;
        public UserRepository(AirbnbDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<AppUser>();
        }
        public async Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression, params string[] includes)
        {
            IQueryable<AppUser> query = expression is not null ?
               _context.AppUsers.Where(expression) : _dbSet.AsQueryable();
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }


        public virtual async Task<AppUser> GetByIdAsync(string id, Expression<Func<AppUser, bool>> expression, params string[] includes)
        {
            IQueryable<AppUser> query = expression is not null ?
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
        public async Task AddAsync(AppUser entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(AppUser entity, bool state = true)
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

        public async Task DeleteAsync(AppUser entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}