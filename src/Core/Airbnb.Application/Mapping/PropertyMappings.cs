using Airbnb.Application.Contracts.v1.Property.Responses;
using Airbnb.Application.Contracts.v1.Property.Responses.NestedPropertyResponses;
using Airbnb.Application.Features.Properties.Commands.Create;
using Airbnb.Application.Features.Properties.Commands.Update;
using Airbnb.Application.Features.User.Commands.Update;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.Property;
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
            CreateMap<Property, PropertyResponse>();
            CreateMap<PropertyImage, PropertyImagesInPropertyResponse>();
            CreateMap<PropertyAmenity, PropertyAmenitiesInPropertyResponse>();
                
            CreateMap<Amenity, AmenityInPropertyAmenities>();
            CreateMap<PropertyGroup, PropertyGroupInPropertyResponse>();
            CreateMap<PropertyType, PropertyTypeInPropertyResponse>();
            CreateMap<CancellationPolicy, CancellationPolicyInPropertyResponse>();
            CreateMap<AirCover, AirCoverInPropertyResponse>();
            CreateMap<AppUser, AppUserInPropertyResponse>();
            CreateMap<PrivacyType, PrivacyTypeInPropertyResponse>();

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
