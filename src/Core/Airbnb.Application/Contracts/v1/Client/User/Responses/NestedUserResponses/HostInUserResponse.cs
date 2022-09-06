using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses
{
    public class HostInUserResponse
    {
        public Guid Id { get; set; }
        // eger rezervasiya 5 den azdisa false, 10 dan azdisa null, 10+da true olsun(host,expert host,superhost)
        public bool IsSuperHost { get; set; } = false;
        public List<GetPropertyResponse> Properties{ get; set; }
        public List<GuestReviewInHostResponse> ReviewsAboutGuests { get; set; }
        public List<ReservationInHostResponse> Reservations { get; set; }

    }
}
