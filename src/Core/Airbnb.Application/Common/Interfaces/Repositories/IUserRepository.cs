using Airbnb.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        //Base entity den miras almirdi deye bunu ayrica yazmali oldum
        Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression, params string[] includes);
        Task<AppUser> GetByIdAsync(string id, Expression<Func<AppUser, bool>> expression, params string[] includes);
        Task AddAsync(AppUser entity);
        void Update(AppUser entity, bool state = true);
        Task DeleteAsync(AppUser entity);
    }
}
