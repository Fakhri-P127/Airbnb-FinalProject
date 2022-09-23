using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class UserIdNotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "User with this Id doesn't exist. Please enter valid Id";
    }
}
