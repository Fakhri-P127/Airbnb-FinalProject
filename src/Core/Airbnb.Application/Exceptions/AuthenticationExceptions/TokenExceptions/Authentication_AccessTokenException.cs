using System.Net;

namespace Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions
{
    public class Authentication_AccessTokenException : Exception, IAuthTokenException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Token Authentication error occured.";

        public string DetailErrorMessage { get; set; }

        public Authentication_AccessTokenException(string errorMessage)
        {
            DetailErrorMessage = errorMessage;
        }
    }
}
