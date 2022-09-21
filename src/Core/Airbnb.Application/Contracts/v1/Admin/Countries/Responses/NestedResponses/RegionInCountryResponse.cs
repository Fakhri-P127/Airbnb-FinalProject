namespace Airbnb.Application.Contracts.v1.Admin.Countries.Responses.NestedResponses
{
    public class RegionInCountryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CountriesCount { get; set; }
    }
}
