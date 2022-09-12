using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationMinNightValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public ReservationMinNightValidationException(byte? minNight,int reservedDays)
        {
            ErrorMessage = $"Minimum night count is {minNight} but you tried to reserve for {reservedDays} days. Your reserved days must be at least {minNight} days long";
        }
    }
}
