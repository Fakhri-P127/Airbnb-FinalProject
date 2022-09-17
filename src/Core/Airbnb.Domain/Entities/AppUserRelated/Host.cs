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
            ReviewsByYou = new();
            ReviewsAboutYourProperty = new();
            Reservations = new();
        }
        public bool IsSuperHost { get; set; }
        public List<Property> Properties { get; set; }
        public List<GuestReview> ReviewsByYou { get; set; }
        public List<PropertyReview> ReviewsAboutYourProperty { get; set; }
        public List<Reservation> Reservations { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
