namespace Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses.NestedResponses
{
    public class AppUserInGuestReviewResponse
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
