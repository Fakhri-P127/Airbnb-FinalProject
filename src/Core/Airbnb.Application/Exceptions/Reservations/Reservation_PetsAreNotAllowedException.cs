using System.Net;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class Reservation_PetsAreNotAllowedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Pets are not allowed in this property.";
    }
}
