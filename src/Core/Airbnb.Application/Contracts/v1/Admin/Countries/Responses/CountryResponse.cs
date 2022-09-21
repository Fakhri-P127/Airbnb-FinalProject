using Airbnb.Application.Contracts.v1.Admin.Countries.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Countries.Responses
{
    public class CountryResponse:BaseResponse
    {
        public string Name { get; set; }
        public RegionInCountryResponse Region { get; set; }
        public List<CityInCountryResponse> Cities { get; set; }
        public List<StateInCountryResponse> States { get; set; }
    }
}
