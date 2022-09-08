using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class AmenityTypeMappings:Profile
    {
        public AmenityTypeMappings()
        {
            CreateMap<AmenityType, AmenityTypeResponse>();
            CreateMap<Amenity, AmenityInAmenityTypeResponse>();
            
        }
    }
}
