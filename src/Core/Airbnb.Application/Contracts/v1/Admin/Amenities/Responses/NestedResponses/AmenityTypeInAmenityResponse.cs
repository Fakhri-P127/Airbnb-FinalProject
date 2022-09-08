using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.Amenities.Responses.NestedResponses
{
    public class AmenityTypeInAmenityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
