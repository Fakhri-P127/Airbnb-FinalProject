using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedPropertyResponses
{
    public class PropertyAmenitiesInPropertyResponse
    {
        public Guid Id { get; set; }
        public AmenityInPropertyAmenities Amenity { get; set; }
    }
    public class AmenityInPropertyAmenities
    {
        public Guid Id { get; set; }
        public Guid AmenityTypeId { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
