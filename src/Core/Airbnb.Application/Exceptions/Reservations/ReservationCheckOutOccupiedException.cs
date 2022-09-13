using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationCheckOutOccupiedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorMessage { get; set; }
        public ReservationCheckOutOccupiedException(DateTime? CheckOut)
        {
            ErrorMessage = $"{CheckOut:dd/MM/yyyy} is already reserved. Please choose checkout date that's not occupied";
        }
    }
}
