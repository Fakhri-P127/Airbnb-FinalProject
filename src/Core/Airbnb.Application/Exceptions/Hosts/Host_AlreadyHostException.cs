using System.Net;

namespace Airbnb.Application.Exceptions.Hosts
{
    public class Host_AlreadyHostException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "You're already a host";
    }
}
