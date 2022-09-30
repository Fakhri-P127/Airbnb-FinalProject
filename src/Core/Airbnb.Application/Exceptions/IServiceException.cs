using System.Net;

namespace Airbnb.Application.Exceptions
{
    public interface IServiceException
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
