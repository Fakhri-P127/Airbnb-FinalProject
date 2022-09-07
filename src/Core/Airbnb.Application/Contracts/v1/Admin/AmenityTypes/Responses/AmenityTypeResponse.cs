using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses
{
    public class AmenityTypeResponse:BaseResponse
    {
        public string Name { get; set; }
        public List<AmenityInAmenityTypeResponse> Amenities { get; set; }
    }
}
