using System.Net;

namespace Airbnb.Application.Exceptions.Cities
{
    public class CityNotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "City with this Id doesn't exist. Please enter valid Id";
    }
}
