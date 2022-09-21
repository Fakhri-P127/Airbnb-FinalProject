namespace Airbnb.Application.Contracts.v1.Admin.Cities.Responses.NestedResponses
{
    public class StateInCityResponse
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }
        public Guid CountryId { get; set; }
        public string Street { get; set; }
        public int PropertiesCount { get; set; }
    }
}
