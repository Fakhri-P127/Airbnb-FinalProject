using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class Amenity:BaseEntity
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AmenityTypeId { get; set; }
        public AmenityType AmenityType { get; set; }
        public List<PropertyAmenity> PropertyAmenities { get; set; }

    }
}
