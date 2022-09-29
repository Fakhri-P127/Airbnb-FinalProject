namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses.NestedStateResponses
{
    public class StateInPropertyResponse
    {
        public RegionInState Region { get; set; }
        public CountryInState Country { get; set; }
        public CityInState City { get; set; }
        public string Street { get; set; }
    }
}
