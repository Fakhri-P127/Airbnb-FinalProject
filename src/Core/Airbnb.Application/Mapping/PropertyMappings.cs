using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedPropertyResponses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses;
using Airbnb.Application.Features.Client.Properties.Commands.Create;
using Airbnb.Application.Features.Client.Properties.Commands.Update;
using Airbnb.Application.Features.Client.User.Commands.Update;
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
    public class PropertyMappings:Profile
    {
        public PropertyMappings()
        {
            CreateMap<Property, GetPropertyResponse>();
            CreateMap<Property, CreatePropertyResponse>();
            CreateMap<PropertyImage, PropertyImagesInPropertyResponse>();
            CreateMap<PropertyAmenity, PropertyAmenitiesInPropertyResponse>();
                
            CreateMap<Amenity, AmenityInPropertyAmenities>();
            CreateMap<PropertyGroup, PropertyGroupInPropertyResponse>();
            CreateMap<PropertyType, PropertyTypeInPropertyResponse>();
            CreateMap<CancellationPolicy, CancellationPolicyInPropertyResponse>();
            CreateMap<AirCover, AirCoverInPropertyResponse>();
            CreateMap<PrivacyType, PrivacyTypeInPropertyResponse>();
            CreateMap<AppUser, HostInPropertyResponse>();
            CreateMap<PropertyReview, PropertyReviewsInReservation>();
            CreateMap<PropertyReview, PropertyReviewInReservationPropertyResponse>();
           
            
            


            CreateMap<CreatePropertyCommand, Property>()
                .ForMember(x => x.PropertyImages, d => d.Ignore())
                .ForMember(x => x.PropertyAmenities, d => d.Ignore());


            CreateMap<UpdatePropertyCommand, Property>()
                .ForMember(x => x.PropertyImages, d => d.Ignore())
               
                .ForMember(x => x.PropertyAmenities, d => d.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
