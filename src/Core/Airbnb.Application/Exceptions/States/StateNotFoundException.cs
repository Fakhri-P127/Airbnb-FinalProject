using System.Net;

namespace Airbnb.Application.Exceptions.States
{
    public class StateNotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "State with this Id doesn't exist. Please enter valid Id";
    }
}
