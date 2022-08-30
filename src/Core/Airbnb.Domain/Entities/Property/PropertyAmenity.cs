using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.Property
{
    public class PropertyAmenity:BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public Guid AmenityId { get; set; }
        public Amenity Amenity { get; set; }

    }
}
