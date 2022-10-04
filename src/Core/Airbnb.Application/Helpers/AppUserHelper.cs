using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Airbnb.Application.Helpers
{
    public static class AppUserHelper
    {
        #region extension methods
        public static string GetUserIdFromClaim(this ClaimsPrincipal User) 
            => User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        
        public static async Task<AppUser> GetUserByIdAsync(this IQueryable<AppUser> users,Guid id,
            CancellationToken cancellationToken=default, params string[] includes)
        {
            return await users.Where(x=>x.Id == id).SetIncludes(includes).FirstOrDefaultAsync(cancellationToken);
        }
        public static IQueryable<AppUser> SetIncludes(this IQueryable<AppUser> query,params string[] includes)
        {
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
        #endregion
        public static string[] AllUserIncludes()
        {
            string[] includes = new[] { "Gender","Host", "AppUserLanguages","AppUserLanguages.Language"/*, "ReviewsByYou"
                , "ReviewsAboutYou", "ReservationsYouMade" */};
            return includes;
        }
     
    }
}
