using System.Net;

namespace Airbnb.Application.Exceptions.CancellationPolicies
{
    public class CancellationPolicyNotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Cancellation policy with this Id doesn't exist. Please enter valid Id";
    }
}
