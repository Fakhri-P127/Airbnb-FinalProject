using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.Base;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class PropertyReview : BaseEntity
    {
        public string Text { get; set; }
        public float OverallScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float CommunicationScore { get; set; }
        public float CheckInScore { get; set; }
        public float AccuracyScore { get; set; }
        public float LocationScore { get; set; }
        public float ValueScore { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
    }
}
