using Airbnb.Application.Contracts.v1.Admin.Cities.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Cities.Responses
{
    public class CityResponse:BaseResponse
    {
        public string Name { get; set; }
        public CountryInCityResponse Country { get; set; }
        public List<StateInCityResponse> States { get; set; }
    }
}
