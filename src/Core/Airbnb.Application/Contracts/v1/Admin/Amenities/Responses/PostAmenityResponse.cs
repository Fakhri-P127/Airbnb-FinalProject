using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Amenities.Responses
{
    public class PostAmenityResponse:BaseResponse
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AmenityTypeInAmenityResponse AmenityType { get; set; }
    }
}
