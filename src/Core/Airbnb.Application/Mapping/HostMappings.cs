using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses;
using Airbnb.Application.Features.Client.Hosts.Commands.Create;
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
    public class HostMappings:Profile
    {
        public HostMappings()
        {
            CreateMap<CreateHostCommand, Host>();
            CreateMap<Host, PostHostResponse>();
            CreateMap<Host, GetHostResponse>();
            CreateMap<AppUser, AppUserInGetHostResponse>();
            CreateMap<Property, PropertyInGetHostResponse>();
            CreateMap<GuestReview, GuestReviewInHostResponse>();
            CreateMap<Reservation, ReservationInHostResponse>();
            CreateMap<PropertyReview, PropertyReviewInHostResponse>();
    }
    }
}
