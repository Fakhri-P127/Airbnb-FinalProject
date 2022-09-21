using Airbnb.Application.Contracts.v1.Admin.Regions.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Regions.Responses
{
    public class RegionResponse:BaseResponse
    {
        public string Name { get; set; }
        public List<CountryInRegionResponse> Countries { get; set; }
        public List<StateInRegionResponse> States { get; set; }
    }
}
