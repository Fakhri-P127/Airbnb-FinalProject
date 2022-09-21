namespace Airbnb.Application.Contracts.v1.Admin.Regions.Responses.NestedResponses
{
    public class CountryInRegionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CitiesCount{ get; set; }
    }
}
