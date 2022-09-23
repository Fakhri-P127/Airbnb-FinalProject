using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_EmailNotConfirmedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Email hasn't been verified yet. First, verify the email that's been sent to your email!";
    }
}
