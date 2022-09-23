using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_EmailAlreadyConfirmedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "This account's email already has been verified.";
    }
}
