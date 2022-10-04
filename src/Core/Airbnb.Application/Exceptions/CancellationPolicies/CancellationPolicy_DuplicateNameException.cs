using System.Net;

namespace Airbnb.Application.Exceptions.CancellationPolicies
{
    public class CancellationPolicy_DuplicateNameException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Cancellation policy with this name already exists";
    }
}
