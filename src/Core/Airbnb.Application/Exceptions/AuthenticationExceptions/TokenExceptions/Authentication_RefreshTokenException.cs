using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions
{
    public class Authentication_RefreshTokenException : Exception, IAuthTokenException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Token Authentication error occured";

        public string DetailErrorMessage { get; set; }

        public Authentication_RefreshTokenException(string errorMessage)
        {
            DetailErrorMessage = errorMessage;
        }
    }
}
