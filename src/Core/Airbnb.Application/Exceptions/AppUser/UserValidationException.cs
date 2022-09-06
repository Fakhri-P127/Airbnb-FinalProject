using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class UserValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorMessage { get; set; }
        
    }
}
