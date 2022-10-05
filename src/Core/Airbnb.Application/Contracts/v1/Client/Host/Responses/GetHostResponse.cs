using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses
{
    public class GetHostResponse:BaseResponse
    {
        public string Status { get; set; }
        public AppUserInGetHostResponse AppUser { get; set; }
        //public List<PropertyInGetHostResponse> Properties { get; set; }
        public string PropertiesUrl { get; set; }
        //public List<GuestReviewInHostResponse> ReviewsByYou { get; set; }
        public string ReviewsByYouUrl { get; set; }
        //public List<PropertyReviewInHostResponse> ReviewsAboutYourProperty { get; set; }
        public string ReviewsAboutYourPropertyUrl { get; set; }
        //public List<ReservationInHostResponse> Reservations { get; set; }
        public string ReservationsUrl { get; set; }
    }
}
