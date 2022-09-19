using System.Net;

namespace Airbnb.Application.Exceptions.AirCovers
{
    public class AirCoverNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Aircover with this Id doesn't exist. Please enter valid Id";
    }
}
