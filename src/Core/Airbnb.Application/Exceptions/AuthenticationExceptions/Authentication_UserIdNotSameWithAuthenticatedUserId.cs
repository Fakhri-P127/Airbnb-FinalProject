using System.Net;

namespace Airbnb.Application.Exceptions.AuthenticationExceptions
{
    public class Authentication_UserIdNotSameWithAuthenticatedUserId : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "You can't change other users data.";
    }
}
