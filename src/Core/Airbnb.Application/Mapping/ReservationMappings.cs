using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses.NestedResponses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Features.Client.Reservations.Commands.Update;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class ReservationMappings : Profile
    {
        public ReservationMappings()
        {
            CreateMap<Reservation, PostReservationResponse>();
            CreateMap<Reservation, GetReservationResponse>();
            CreateMap<PropertyReview, PropertyReviewInReservationResponse>();
            CreateMap<GuestReview, GuestReviewInReservationResponse>();

            CreateMap<CreateReservationCommand, Reservation>();

            CreateMap<UpdateReservationCommand, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HostId, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
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
