using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.Reservation.Parameters
{
    public class ReservationParameters : BaseQueryStringParameters
    {
        public override int PageSize { get; set; } = 4;
        public DateTime? MinCheckInDate { get; set; }
        public DateTime? MaxCheckInDate { get; set; } 
        public DateTime? MinCheckOutDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; } 
        public int? MinTotalPrice { get; set; }
        public int? MaxTotalPrice { get; set; }
        public int? Status { get; set; }
        public Guid? PropertyId { get; set; }
        public Guid? AppUserId { get; set; }
        public Guid? HostId { get; set; }
    }
}
