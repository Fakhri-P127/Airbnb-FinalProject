using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class UserNotFoundValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage  => "Email or password is incorrect.";
    }
}
