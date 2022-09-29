using System.Net;

namespace Airbnb.Application.Exceptions.PropertyGroups
{
    public class PropertyGroupImageValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public PropertyGroupImageValidationException()
        {
            ErrorMessage = "Image size is too big";
        }
    }
}
