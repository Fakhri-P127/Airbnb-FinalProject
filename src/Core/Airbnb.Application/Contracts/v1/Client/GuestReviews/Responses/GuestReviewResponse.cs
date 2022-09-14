using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses.NestedResponses;
using Airbnb.Domain.Entities.AppUserRelated;

namespace Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses
{
    public class GuestReviewResponse:BaseResponse
    {
        public string Text { get; set; }
        public float GuestScore { get; set; }
        public HostInGuestReviewResponse Host { get; set; }
        // guest
        public AppUserInGuestReviewResponse AppUser { get; set; }
        // booking e gore bunu yaza bilsin.
        public ReservationInGuestReviewResponse Reservation { get; set; }
    }
}
