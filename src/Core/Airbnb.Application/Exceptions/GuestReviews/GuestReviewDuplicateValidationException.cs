using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.GuestReviews
{
    public class GuestReviewDuplicateValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; set; }
        public GuestReviewDuplicateValidationException(Guid reservationId)
        {
            ErrorMessage = $"You have already written a guest review for this reservation({reservationId}).";
        }
    }
}
