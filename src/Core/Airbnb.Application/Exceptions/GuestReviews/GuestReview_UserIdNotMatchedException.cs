using System.Net;

namespace Airbnb.Application.Exceptions.GuestReviews
{
    public class GuestReview_HostIdNotMatchedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage { get; set; }
        public GuestReview_HostIdNotMatchedException(Guid guestReviewHostId, Guid reservationHostId)
        {
            ErrorMessage = $"Host Id of Guest Review doesn't match with the host Id of Reservation. {guestReviewHostId} ❌ {reservationHostId}";
        }
    }
}
