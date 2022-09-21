using System.Net;

namespace Airbnb.Application.Exceptions.Cities
{
    public class CityDoesntBelongToSpecificiedCountryException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public CityDoesntBelongToSpecificiedCountryException(string cityName, string countryName)
        {
            ErrorMessage = $"{cityName}(city) doesn't belong to {countryName}(country). Please choose your country or city correctly.";
        }
    }
}
