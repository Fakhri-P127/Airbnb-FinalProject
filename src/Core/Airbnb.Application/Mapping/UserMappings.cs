using Airbnb.Application.Contracts.v1;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses;
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
            // Null olub olmadigini yigishdirdim chunki bele Include etmeden istifade ede bilirik.
            // hamiya o url gonderilecek amma ichinde reservationin olub olmadigi sorgu gonderende bilinecek
            // mence reservation i join etmekdense bele daha yaxshidi.
            CreateMap<AppUser, UserResponse>()
                 .ForMember(dest => dest.ReservationYouMadeUrl, opt =>
                 {
                     //opt.PreCondition(x => x.ReservationsYouMade is not null 
                     //&& x.ReservationsYouMade.Any());
                     opt
                     .MapFrom(src => $"{ApiRoutes.BaseUrl}/{ApiRoutes.Reservations.Name}?appUserId={src.Id}");
                 })
                  .ForMember(dest => dest.ReviewsByYouUrl, opt =>
                  {
                      //opt.PreCondition(x => x.ReviewsByYou is not null && x.ReviewsByYou.Any());
                      opt
                      .MapFrom(src => $"{ApiRoutes.BaseUrl}/{ApiRoutes.PropertyReviews.Name}?appUserId={src.Id}");
                  })
                   .ForMember(dest => dest.ReviewsAboutYouUrl, opt =>
                   {
                       //opt.PreCondition(x => x.ReviewsAboutYou is not null && x.ReviewsAboutYou.Any());
                       opt
                       .MapFrom(src => $"{ApiRoutes.BaseUrl}/{ApiRoutes.GuestReviews.Name}?appUserId={src.Id}");
                   });

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
