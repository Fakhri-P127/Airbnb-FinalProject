using Airbnb.Application.Contracts.v1.User.Responses;
using Airbnb.Application.Features.User.Commands.Update;
using Airbnb.Domain.Entities.Common;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<AppUser, UserResponse>();

            CreateMap<UpdateUserCommand, AppUser>()
                .ForMember(x => x.ProfilPicture, d => d.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //CreateMap<AppUser, UserResponse>();

            CreateMap<Gender, GenderInAppUser>();
        }
    }
}
