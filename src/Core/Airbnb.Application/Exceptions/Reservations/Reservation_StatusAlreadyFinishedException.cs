using System.Net;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class Reservation_StatusAlreadyFinishedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "This reservation is already finished, you can't change the status anymore";
    }
}
