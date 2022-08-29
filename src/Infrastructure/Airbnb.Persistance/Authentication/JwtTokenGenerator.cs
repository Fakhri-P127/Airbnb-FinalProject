using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Domain.Entities;
using Airbnb.Persistance.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(UserManager<AppUser> userManager,JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;

        }
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            await Task.CompletedTask;

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.UserData,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.AuthenticationInstant,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes).ToString()),
            };

           //IList<string> roles = await _userManager.GetRolesAsync(user);
           // claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding
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
