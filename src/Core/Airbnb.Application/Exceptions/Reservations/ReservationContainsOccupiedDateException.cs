using System.Net;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationContainsOccupiedDateException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "This timeframe includes already reserved days. Please select dates that doesn't contain reserved days";
    }
}
