namespace Airbnb.Application.Contracts.v1.Admin.Regions.Responses.NestedResponses
{
    public class StateInRegionResponse
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public string Street { get; set; }
        public int PropertiesCount { get; set; }
    }
}
