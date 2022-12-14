using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses.NestedResponses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Features.Client.Reservations.Commands.Update;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class ReservationMappings : Profile
    {
        public ReservationMappings()
        {
            CreateMap<Reservation, PostReservationResponse>()
                .ForMember(dest => dest.Status, opt => opt
                .MapFrom(dest => ReservationHelpers.ChangeStatusToString(dest.Status)));

            CreateMap<Reservation, GetReservationResponse>()
                .ForMember(dest => dest.Status, opt => opt
                .MapFrom(dest=> ReservationHelpers.ChangeStatusToString(dest.Status)));

            CreateMap<PropertyReview, PropertyReviewInReservationResponse>();
            CreateMap<GuestReview, GuestReviewInReservationResponse>();

            CreateMap<CreateReservationCommand, Reservation>()
                .ForMember(dest => dest.PetCount, opt => opt.Ignore())
                .ForMember(dest=>dest.PropertyId,opt=>opt.Ignore());

            CreateMap<UpdateReservationCommand, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.HostId, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
                .ForMember(dest=>dest.PetCount,opt=>opt.Ignore())
                .ForMember(dest => dest.AdultCount, opt =>
                 {
                     opt.PreCondition((src, dest, context) => src.AdultCount != dest.AdultCount);
                     opt.MapFrom(src => src.AdultCount);
                 })
                .ForMember(dest => dest.ChildCount, opt =>
                 {
                     opt.PreCondition((src, dest, context) => src.ChildCount != dest.ChildCount);
                     opt.MapFrom(src => src.ChildCount);
                 })
                .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
            
        }
    }
}
