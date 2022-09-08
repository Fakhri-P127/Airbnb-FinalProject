using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses
{
    public class AmenityInPropertyAmenities
    {

        public Guid Id { get; set; }
        public Guid AmenityTypeId { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
