using Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedPropertyResponses;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses;
using Airbnb.Application.Features.Client.User.Commands.Update;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
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

            CreateMap<Gender, GenderInUserResponse>();
            CreateMap<Host, HostInUserResponse>();
            CreateMap<AppUserLanguage, AppUserLanguageInUserResponse>();
            CreateMap<Reservation, ReservationInUserResponse>();
            CreateMap<PropertyReview, PropertyReviewsInUserResponse>();
            CreateMap<GuestReview, GuestReviewInUserResponse>();
            CreateMap<Language, LanguageInAppUserLanguage>();
            CreateMap<PropertyReview, PropertyReviewsInUserResponse>();
            

        }
    }
}
