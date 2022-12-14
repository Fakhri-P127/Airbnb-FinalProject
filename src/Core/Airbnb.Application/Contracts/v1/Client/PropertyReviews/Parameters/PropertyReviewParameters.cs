using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.PropertyReviews.Parameters
{
    public class PropertyReviewParameters:BaseQueryStringParameters
    {
        public override int PageSize { get; set; } = 4;
        public Guid? AppUserId { get; set; }
        public Guid? PropertyId { get; set; }
        public Guid? HostId { get; set; }
        public Guid? ReservationId { get; set; }
    }
}
