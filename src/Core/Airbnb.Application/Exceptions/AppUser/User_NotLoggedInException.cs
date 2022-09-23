using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_NotLoggedInException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "You're not logged in to use this feature";
    }
}
