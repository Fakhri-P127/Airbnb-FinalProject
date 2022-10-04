using System.Net;

namespace Airbnb.Application.Exceptions.AmenityTypes
{
    public class AmenityType_DuplicateNameException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Amenity type with this name already exists.";
    }
}
