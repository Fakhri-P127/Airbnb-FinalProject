using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class RefreshTokenMappings:Profile
    {
        public RefreshTokenMappings()
        {
            CreateMap<RefreshToken, RefreshTokenResponse>();
        }
    }
}
