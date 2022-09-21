using System.Net;

namespace Airbnb.Application.Exceptions.Regions
{
    public class RegionNotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Region with this Id doesn't exist. Please enter valid Id";
    }
}
