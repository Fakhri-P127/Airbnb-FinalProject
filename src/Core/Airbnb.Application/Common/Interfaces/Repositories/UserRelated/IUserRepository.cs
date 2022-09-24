using Airbnb.Domain.Entities.AppUserRelated;
using System.Linq.Expressions;

namespace Airbnb.Application.Common.Interfaces.Repositories.UserRelated
{
    public interface IUserRepository
    {
        //Base entity den miras almirdi deye bunu ayrica yazmali oldum
        Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression, params string[] includes);
        Task<AppUser> GetByIdAsync(Guid id, Expression<Func<AppUser, bool>> expression, params string[] includes);
        Task<AppUser> GetSingleAsync(Expression<Func<AppUser, bool>> expression, params string[] includes);
        Task AddAsync(AppUser entity);
        void Update(AppUser entity, bool state = true);
        Task DeleteAsync(AppUser entity);
    }
}
