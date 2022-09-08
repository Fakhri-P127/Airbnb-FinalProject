using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses
{
    public class PropertyAmenitiesInPropertyResponse
    {
        public Guid Id { get; set; }
        public AmenityInPropertyAmenities Amenity { get; set; }
    }
  
}
