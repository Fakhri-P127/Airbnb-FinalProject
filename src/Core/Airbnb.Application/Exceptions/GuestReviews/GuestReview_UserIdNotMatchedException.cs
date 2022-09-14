using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.GuestReviews
{
    public class GuestReview_UserIdNotMatchedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public GuestReview_UserIdNotMatchedException(string guestReviewUserId, string reservationUserId)
        {
            ErrorMessage = $"User Id of Guest Review doesn't match with the Id of Reservation. {guestReviewUserId} ❌ {reservationUserId}";
        }
    }
}
