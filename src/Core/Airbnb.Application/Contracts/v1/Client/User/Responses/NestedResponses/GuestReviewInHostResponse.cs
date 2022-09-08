using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses
{
    public class GuestReviewInHostResponse
    {
        public string Text { get; set; }
        public float GuestScore { get; set; }
        // guest id
        public string AppUserId { get; set; }
     
        // booking e gore bunu yaza bilsin.
        public Guid ReservationId { get; set; }
 
    }
}
