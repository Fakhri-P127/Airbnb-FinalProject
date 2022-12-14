namespace Airbnb.Application.Contracts.v1.Client.Reservation.Responses.NestedResponses
{
    public class PropertyReviewInReservationResponse
    {
        public string Text { get; set; }
        public float OverallScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float CommunicationScore { get; set; }
        public float CheckInScore { get; set; }
        public float AccuracyScore { get; set; }
        public float LocationScore { get; set; }
        public float ValueScore { get; set; }
        //public Guid AppUserId { get; set; }
      
    }
}
