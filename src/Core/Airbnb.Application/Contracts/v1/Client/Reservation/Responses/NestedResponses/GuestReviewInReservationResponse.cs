using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Reservation.Responses.NestedResponses
{
    public class GuestReviewInReservationResponse
    {
        public string Text { get; set; }
        public float GuestScore { get; set; }
    }
}
