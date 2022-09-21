using System.Net;

namespace Airbnb.Application.Exceptions.Countries
{
    public class CountryDoesntBelongToSpecificiedRegionException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public CountryDoesntBelongToSpecificiedRegionException(string countryName,string regionName)
        {
            ErrorMessage = $"{countryName} doesn't belong to {regionName} region. Please choose your region or country correctly.";
        }
    }
}
