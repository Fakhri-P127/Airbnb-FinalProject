using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses
{
    public class GuestReviewInUserResponse
    {
        public string Text { get; set; }
        public float GuestScore { get; set; }
        // guest id
        public Guid HostId { get; set; }

        // booking e gore bunu yaza bilsin.
        public Guid ReservationId { get; set; }
    }
}
