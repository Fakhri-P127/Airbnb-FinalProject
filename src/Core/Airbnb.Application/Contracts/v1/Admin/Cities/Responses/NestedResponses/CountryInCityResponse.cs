using Airbnb.Application.Contracts.v1.Admin.Countries.Responses.NestedResponses;

namespace Airbnb.Application.Contracts.v1.Admin.Cities.Responses.NestedResponses
{
    public class CountryInCityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CitiesCount { get; set; }
        public Guid RegionId { get; set; }
    }
}
