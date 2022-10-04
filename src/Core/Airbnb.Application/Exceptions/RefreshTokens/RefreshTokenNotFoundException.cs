using System.Net;

namespace Airbnb.Application.Exceptions.RefreshTokens
{
    public class RefreshTokenNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Refresh token with this Id doesn't exist.";
    }
}
