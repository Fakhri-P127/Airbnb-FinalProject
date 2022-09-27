using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses;
using Airbnb.Application.Features.Client.Hosts.Commands.Create;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class HostMappings:Profile
    {
        public HostMappings()
        {
            CreateMap<CreateHostCommand, Host>()
                .ForMember(dest=>dest.Status,opt=>opt.MapFrom(src=> (int)Enum_HostStatus.BasicHost));
            CreateMap<Host, PostHostResponse>()
                .ForMember(dest=>dest.Status, opt=>opt
                .MapFrom(src=>HostHelper.ChangeStatusToString(src.Status)));
            CreateMap<Host, GetHostResponse>()
                .ForMember(dest => dest.Status, opt => opt
                .MapFrom(src => HostHelper.ChangeStatusToString(src.Status)));
            CreateMap<AppUser, AppUserInGetHostResponse>();
            CreateMap<Property, PropertyInGetHostResponse>();
            CreateMap<GuestReview, GuestReviewInHostResponse>();
            CreateMap<Reservation, ReservationInHostResponse>();
            CreateMap<PropertyReview, PropertyReviewInHostResponse>();
    }
    }
}
