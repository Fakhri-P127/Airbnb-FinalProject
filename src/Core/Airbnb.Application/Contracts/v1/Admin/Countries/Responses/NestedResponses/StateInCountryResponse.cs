namespace Airbnb.Application.Contracts.v1.Admin.Countries.Responses.NestedResponses
{
    public class StateInCountryResponse
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }
        public Guid CityId { get; set; }
        public string Street { get; set; }
        public int PropertiesCount { get; set; }
    }
}
