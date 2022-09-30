using System.Net;

namespace Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions
{
    public class Authentication_PrincipalException : Exception, IAuthTokenException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public string ErrorMessage => "Token Authentication error occured.";
        public string DetailErrorMessage => "Either princal is empty or token expired. Please try again.";
    }
}
