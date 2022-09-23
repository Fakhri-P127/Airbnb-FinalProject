using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class UserNotFoundValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; set; } = "Email or password is incorrect.";
    }
}
