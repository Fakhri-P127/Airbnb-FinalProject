using System.Net;

namespace Airbnb.Application.Exceptions.Hosts
{
    public class HostNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; set; }
        public HostNotFoundException(Guid id)
        {
            ErrorMessage = $"Host with this Id({id}) doesn't exist";
        }
    }
}
