using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Authentication.CustomFrameworkClasses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Airbnb.Persistance.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(CustomUserManager<AppUser> userManager,JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;

        }
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.UserData,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.AuthenticationInstant,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes).ToString()),
            };

            IList<string> roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_jwtSettings.Secret)),SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new(
                claims: claims,
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials:signingCredentials
                );

           return new JwtSecurityTokenHandler().WriteToken(securityToken);
            
        }
    }
}
