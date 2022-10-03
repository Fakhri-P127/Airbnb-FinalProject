using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces.Authentication;
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
            return newRefreshToken.Id.ToString();// tokenin ozunu primary key olaraq Id etmishem.
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
    }
}
