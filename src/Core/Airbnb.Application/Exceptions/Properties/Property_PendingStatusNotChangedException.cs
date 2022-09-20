using System.Net;

namespace Airbnb.Application.Exceptions.Properties
{
    public class Property_PendingStatusNotChangedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Host is not verified. Property status has not changed.";
    }
}
