using System.Security.Claims;

namespace Airbnb.Application.Common.Interfaces.Authentication
{
    public interface IAccessTokenGenerator
    {
        Task<string> GenerateJwtAccessTokenAsync(List<Claim> claims);

    }
}
