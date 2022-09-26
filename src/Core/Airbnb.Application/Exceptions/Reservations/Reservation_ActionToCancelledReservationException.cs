using System.Net;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class Reservation_ActionToCancelledReservationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "This reservation is already cancelled, you can't change it's data anymore.";
    }
}
