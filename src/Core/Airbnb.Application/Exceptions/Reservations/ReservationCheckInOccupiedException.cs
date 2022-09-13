using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationCheckInOccupiedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorMessage { get; set; }
        public ReservationCheckInOccupiedException(DateTime? CheckIn)
        {
            ErrorMessage = $"{CheckIn:dd/MM/yyyy} is already reserved. Please choose checkin date that's not occupied";
        }
    }
}
