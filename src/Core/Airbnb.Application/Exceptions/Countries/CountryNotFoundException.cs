using System.Net;

namespace Airbnb.Application.Exceptions.Countries
{
    public class CountryNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Country with this Id doesn't exist. Please enter valid Id";
    }
}
