using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses.NestedResponses
{
    public class ReservationInGuestReviewResponse
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public int PetCount { get; set; }
        public int TotalPrice { get; set; }
        public Guid PropertyId { get; set; }

    }
}
