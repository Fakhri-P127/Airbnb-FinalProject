using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Reservations
{
    public class ReservationNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get;set; }
        public ReservationNotFoundException(Guid id)
        {
            ErrorMessage= $"Reservation with this Id({id}) doesn't exist.";
        }
    }
}
