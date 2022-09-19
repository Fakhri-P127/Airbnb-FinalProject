using System.Net;

namespace Airbnb.Application.Exceptions.AmenityTypes
{
    public class AmenityTypeNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Amenity type with this Id doesn't exist. Please enter valid Id";
    }
}

