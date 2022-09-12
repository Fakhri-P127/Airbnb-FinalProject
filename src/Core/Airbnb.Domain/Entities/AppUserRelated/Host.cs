using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class Host:BaseEntity
    {
        public Host()
        {
            Properties = new();
            ReviewsAboutGuests = new();
            Reservations = new();
        }
        public bool IsSuperHost { get; set; }

        public List<Property> Properties { get; set; }
        //prop list reviews by you and reviews you gotten. AppUser de 2 bashqa review, hostda 2 bashqa review.
        // 2 booking olmalidi obshi. Hostdaki senin propertylerive olunan booking, travelerde ise etdiyi bookingler.
        //public List<Review> PropertyReviews { get; set; }
        public List<GuestReview> ReviewsAboutGuests { get; set; }
        //public List<Review> ReviewsByYou { get; set; }

        // Bu Reservation unun relation u nu yigishdirmaq olar, Reservation da HostId saxlamaga ehtiyac yoxdu
        // her propertynin reservationlari buna beraber olsun.
        public List<Reservation> Reservations { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
