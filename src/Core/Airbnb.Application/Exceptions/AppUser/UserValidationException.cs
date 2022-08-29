using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class UserValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        //public string ErrorMessage => "Invalid error";
        public string ErrorMessage { get; set; }
        //public CreateUserFailureException(string message) : base(message)
        //{

        //}
    }
}
