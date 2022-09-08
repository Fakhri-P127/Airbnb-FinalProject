using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses
{
    public class GetHostResponse:BaseResponse
    {
        public bool IsSuperHost { get; set; }
        public AppUserInGetHostResponse AppUser { get; set; }

        public List<PropertyInGetHostResponse> Properties { get; set; }
        //prop list reviews by you and reviews you gotten. AppUser de 2 bashqa review, hostda 2 bashqa review.
        // 2 booking olmalidi obshi. Hostdaki senin propertylerive olunan booking, travelerde ise etdiyi bookingler.
        public List<GuestReviewInHostResponse> ReviewsAboutGuests { get; set; }
        //public List<Review> ReviewsByYou { get; set; }
        public List<ReservationInHostResponse> Reservations { get; set; }
 
    }
}
