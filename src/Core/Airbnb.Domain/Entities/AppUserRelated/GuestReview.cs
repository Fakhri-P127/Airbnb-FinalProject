using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class GuestReview : BaseEntity
    {
        public string Text { get; set; }
        public float GuestScore { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
        // guest id
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        // booking e gore bunu yaza bilsin.
        public Guid ReservationId { get; set; }
        public Reservation  Reservation{ get; set; }
    }
}
