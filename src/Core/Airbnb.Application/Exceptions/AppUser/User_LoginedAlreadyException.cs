using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_LoginedAlreadyException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "You're already logged in";
    }
}
