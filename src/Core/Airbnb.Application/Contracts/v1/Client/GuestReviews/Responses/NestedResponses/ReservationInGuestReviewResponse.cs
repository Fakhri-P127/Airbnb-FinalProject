namespace Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses.NestedResponses
{
    public class ReservationInGuestReviewResponse
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int TotalPrice { get; set; }
        public Guid PropertyId { get; set; }
    }
}
