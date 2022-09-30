using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Airbnb.Application.Features.Client.Authentication.Commands.GenerateRefreshToken
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, AuthSuccessResponse>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unit;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ITokenGeneratorService _tokenGenerator;

        public CreateRefreshTokenCommandHandler(CustomUserManager<AppUser> userManager
            , IUnitOfWork unit, TokenValidationParameters tokenValidationParameters,
            ITokenGeneratorService tokenGenerator)
        {
            _userManager = userManager;
            _unit = unit;
            _tokenValidationParameters = tokenValidationParameters;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<AuthSuccessResponse> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal claimPrincipal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (claimPrincipal == null) throw new Authentication_PrincipalException();

            long expiryDateUnix = long.Parse(claimPrincipal.Claims.FirstOrDefault(x => x.Type
            == JwtRegisteredClaimNames.Exp).Value);

            DateTime expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);
            if (expiryDateUtc > DateTime.UtcNow)
                throw new Authentication_AccessTokenException("Access token hasn't expired yet");

            RefreshToken storedRefreshToken = await _unit.RefreshTokenRepository
                .GetByIdAsync(request.RefreshToken, null);

            CheckRefreshTokenExceptions(claimPrincipal, storedRefreshToken);
            // her sheyi deyishmirik deye ehtiyac yoxdu modified olmasina
            //_context.Entry(storedRefreshToken).State = EntityState.Unchanged;
            _unit.RefreshTokenRepository.Update(storedRefreshToken, false);

            storedRefreshToken.HasBeenUsed = true;//yoxla gor bu isheleyecek, savechange bashqa metoddadi

            AppUser user = await _userManager.GetUserAsync(claimPrincipal);
            // bunu yighishdirib claimsPrincipalida
            // saxlmaq olarmi yoxla, mentiqce olmaz mence
            List<Claim> claims = await _tokenGenerator.CreateClaims(user);
            return new AuthSuccessResponse()
            {
                AccessToken = await _tokenGenerator.GenerateJwtAccessTokenAsync(claims),
                RefreshToken = await _tokenGenerator.GenerateRefreshTokenAsync(claims, user.Id)
            };
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            _tokenValidationParameters.ValidateLifetime = false;
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, _tokenValidationParameters
                    , out SecurityToken validatedToken);
                if (!IsJwtWithValidSecurityAlghoritm(validatedToken))
                    throw new Authentication_AccessTokenException("Security alghoritm is false.");
                return principal;
            }
            catch (Exception)
            {
                throw new Authentication_AccessTokenException("Token couldn't be validated. Jwt format or token itself is false. Jwt tokens must have three segments (JWS) or five segments (JWE)");
            }
            //_tokenValidationParameters.ValidateLifetime = true;

        }
        private static bool IsJwtWithValidSecurityAlghoritm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256
                , StringComparison.InvariantCultureIgnoreCase);
        }
        private static void CheckRefreshTokenExceptions(ClaimsPrincipal claimPrincipal, RefreshToken storedRefreshToken)
        {
            if (storedRefreshToken is null)
                throw new Authentication_RefreshTokenException("This refresh token does not exist");
            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                throw new Authentication_RefreshTokenException("This refresh token has expired");
            if (storedRefreshToken.IsRevoked)
                throw new Authentication_RefreshTokenException("This refresh token has been revoked");
            if (storedRefreshToken.HasBeenUsed)
                throw new Authentication_RefreshTokenException("This refresh token has been used");

            string jti = claimPrincipal.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedRefreshToken.JwtId != jti)
                throw new Authentication_RefreshTokenException("This refresh token does not match this Jwt");
        }
    }
}
