using System.Net;

namespace Airbnb.Application.Exceptions.Hosts
{
    public class HostNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Host with this Id doesn't exist";
    }
}
