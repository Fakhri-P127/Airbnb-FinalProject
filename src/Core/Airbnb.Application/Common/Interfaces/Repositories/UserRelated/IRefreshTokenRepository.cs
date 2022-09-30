using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Domain.Entities.AppUserRelated;

namespace Airbnb.Application.Common.Interfaces.Repositories.UserRelated
{
    public interface IRefreshTokenRepository:IGenericRepository<RefreshToken>
    {
    }
}
