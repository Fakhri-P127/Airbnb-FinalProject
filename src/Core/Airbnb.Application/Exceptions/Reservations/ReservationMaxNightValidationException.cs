using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationMaxNightValidationException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public ReservationMaxNightValidationException(byte? maxNight, int reservedDays)
        {
            ErrorMessage = $"Maximum night count is {maxNight} but you tried to reserve it for {reservedDays} days. Your reserved days must be less or equal to {maxNight} days long";
        }
    }
}
