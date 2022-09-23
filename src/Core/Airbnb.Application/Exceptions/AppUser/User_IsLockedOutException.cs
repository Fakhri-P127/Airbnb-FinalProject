using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_IsLockedOutException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Locked;

        public string ErrorMessage => "This user is locked out due to too many attempts of wrong loging in";
    }
}
