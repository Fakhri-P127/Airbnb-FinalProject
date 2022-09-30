using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Context;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Airbnb.Persistance.Authentication
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly AirbnbDbContext _context;

        public TokenGeneratorService(CustomUserManager<AppUser> userManager, JwtSettings jwtSettings,
            AirbnbDbContext context)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _context = context;
        }
        public async Task<string> GenerateJwtAccessTokenAsync(List<Claim> claims)
        {
            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new(
                claims: claims,
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: signingCredentials
                );
            
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }
        public async Task<string> GenerateRefreshTokenAsync(List<Claim> claims, Guid userId)
        {
            //IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            RefreshToken newRefreshToken = new()
            {
                JwtId = claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value,
                AppUserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };
            await _context.RefreshTokens.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();
            return newRefreshToken.Id.ToString();
        }
        public async Task<List<Claim>> CreateClaims(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now
                .AddMinutes(_jwtSettings.ExpiryMinutes).ToString()),
            };

            IList<string> roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
        //public bool IsValidJwtToken(string jwtToken)
        //{
        //    string[] jwtSplitted = jwtToken.Split("\\.");
        //    if (jwtSplitted.Length != 3) throw new Authentication_AccessTokenException("Jwt format is wrong. Jwt tokens must have three segments (JWS) or five segments (JWE)");
        //    try
        //    {
        //        //var jsonFirstPart = Encoding.(jwtSplitted[0]);
                
        //    }
            
        //    return true;
        //}
        //public async Task<string> GenerateAnotherRefreshTokenAsync(string token, string refreshToken)
        //{
        //    ClaimsPrincipal claimPrincipal = GetPrincipalFromExpiredToken(token);
        //    if (claimPrincipal == null) throw new Authentication_PrincipalException();

        //    long expiryDateUnix = long.Parse(claimPrincipal.Claims.FirstOrDefault(x => x.Type
        //    == JwtRegisteredClaimNames.Exp).Value);

        //    DateTime expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        //        .AddSeconds(expiryDateUnix);
        //    if (expiryDateUtc > DateTime.UtcNow)
        //        throw new Authentication_AccessTokenException("Access token hasn't expired yet");

        //    RefreshToken storedRefreshToken = await _context.RefreshTokens
        //    .FirstOrDefaultAsync(x => x.Id.ToString() == refreshToken);

        //    CheckRefreshTokenExceptions(claimPrincipal, storedRefreshToken);
        //    // her sheyi deyishmirik deye ehtiyac yoxdu modified olmasina
        //    _context.Entry(storedRefreshToken).State = EntityState.Unchanged;
        //    storedRefreshToken.HasBeenUsed = true;//yoxla gor bu isheleyecek, savechange bashqa metoddadi

        //    AppUser user = await _userManager.GetUserAsync(claimPrincipal);
        //    // id si tokenin ozudu
        //    return await GenerateRefreshTokenAsync(user);
        //}


        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //    JwtSecurityTokenHandler tokenHandler = new();
        //    _tokenValidationParameters.ValidateLifetime = false;
        //    ClaimsPrincipal principal = tokenHandler.ValidateToken(token, _tokenValidationParameters
        //        , out SecurityToken validatedToken);
        //    //_tokenValidationParameters.ValidateLifetime = true;
        //    if (!IsJwtWithValidSecurityAlghoritm(validatedToken)) return null;

        //    return principal;
        //}
        //public bool IsJwtWithValidSecurityAlghoritm(SecurityToken validatedToken)
        //{
        //    return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
        //        jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256
        //        , StringComparison.InvariantCultureIgnoreCase);
        //}

        //private static void CheckRefreshTokenExceptions(ClaimsPrincipal claimPrincipal, RefreshToken storedRefreshToken)
        //{
        //    if (storedRefreshToken is null)
        //        throw new Authentication_RefreshTokenException("This refresh token does not exist");
        //    if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        //        throw new Authentication_RefreshTokenException("This refresh token has expired");
        //    if (storedRefreshToken.IsRevoked)
        //        throw new Authentication_RefreshTokenException("This refresh token has been revoked");
        //    if (storedRefreshToken.HasBeenUsed)
        //        throw new Authentication_RefreshTokenException("This refresh token has been used");

        //    string jti = claimPrincipal.Claims
        //        .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        //    if (storedRefreshToken.JwtId != jti)
        //        throw new Authentication_RefreshTokenException("This refresh token does not match this Jwt");
        //}

    }
}
