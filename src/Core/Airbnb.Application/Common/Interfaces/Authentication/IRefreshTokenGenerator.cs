using System.Security.Claims;

namespace Airbnb.Application.Common.Interfaces.Authentication
{
    public interface IRefreshTokenGenerator
    {
        //Task<string> GenerateAnotherRefreshTokenAsync(string token, string refreshToken);
        Task<string> GenerateRefreshTokenAsync(List<Claim> claims, Guid userId);
        //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        //bool IsJwtWithValidSecurityAlghoritm(SecurityToken validatedToken);
    }
}
