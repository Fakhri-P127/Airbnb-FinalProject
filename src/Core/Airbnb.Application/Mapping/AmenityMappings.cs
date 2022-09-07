using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses.NestedAmenityResponses;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Update;
using Airbnb.Application.Features.Admin.Amenities.Commands.Create;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class AmenityMappings:Profile
    {
        public AmenityMappings()
        {
            CreateMap<CreateAmenityCommand, Amenity>();
            CreateMap<UpdateAmenityCommand, Amenity>();
            CreateMap<Amenity, PostAmenityResponse>();
            CreateMap<Amenity, GetAmenityResponse>();

            CreateMap<AmenityType, AmenityTypeInAmenityResponse>();
            CreateMap<PropertyAmenity, PropertyAmenityInAmenityResponse>();


        }
    }
}
