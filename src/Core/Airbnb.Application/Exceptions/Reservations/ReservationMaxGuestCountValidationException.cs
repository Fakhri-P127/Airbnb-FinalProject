using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationMaxGuestCountValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public string ErrorMessage { get; set; }
        public ReservationMaxGuestCountValidationException(byte? maxGuestCount, int totalGuestCount)
        {
            ErrorMessage = $"Maximum guest count is {maxGuestCount} but you tried to reserve it with {totalGuestCount} guests. Total guest count must be less or equal to {maxGuestCount} guests";

        }
    }
}
