using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses.NestedResponses;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Create;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Update;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class GuestReviewMappings : Profile
    {
        public GuestReviewMappings()
        {
            CreateMap<GuestReview, GuestReviewResponse>();
            CreateMap<Host, HostInGuestReviewResponse>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.AppUser.Firstname} {src.AppUser.Lastname}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber));

            CreateMap<AppUser, AppUserInGuestReviewResponse>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.Firstname} {src.Lastname}"));
            CreateMap<Reservation, ReservationInGuestReviewResponse>();

            CreateMap<CreateGuestReviewCommand, GuestReview>();
            CreateMap<UpdateGuestReviewCommand, GuestReview>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.GuestScore, opt =>
                 {
                     opt.PreCondition(src => src.GuestScore != null);
                     opt.MapFrom(src => (float)src.GuestScore);
                 })
                 .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
