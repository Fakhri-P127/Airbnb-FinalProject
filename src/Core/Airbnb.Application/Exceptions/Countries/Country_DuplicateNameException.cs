using System.Net;

namespace Airbnb.Application.Exceptions.Countries
{
    public class Country_DuplicateNameException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; set; }
        public Country_DuplicateNameException(string countryName)
        {
            ErrorMessage = $"Country name {countryName} already exists. Please enter non existent country name";
        }
    }
}
