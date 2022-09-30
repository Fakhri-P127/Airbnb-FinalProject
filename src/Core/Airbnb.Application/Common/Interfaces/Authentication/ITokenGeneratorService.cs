using Airbnb.Domain.Entities.AppUserRelated;
using System.Security.Claims;

namespace Airbnb.Application.Common.Interfaces.Authentication
{
    public interface ITokenGeneratorService: IAccessTokenGenerator,IRefreshTokenGenerator
    {
        Task<List<Claim>> CreateClaims(AppUser user);
    }
}
