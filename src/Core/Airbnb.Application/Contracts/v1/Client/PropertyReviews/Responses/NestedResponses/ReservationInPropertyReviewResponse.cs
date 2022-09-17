using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses.NestedResponses
{
    public class ReservationInPropertyReviewResponse
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        //public Guid PropertyId { get; set; }
        //public Guid HostId { get; set; }
        public int TotalPrice { get; set; }

    }
}
