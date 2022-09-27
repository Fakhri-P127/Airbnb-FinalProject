using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.PropertyRelated;

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
        public int Status { get; set; }
        public List<Property> Properties { get; set; }
        public List<GuestReview> ReviewsByYou { get; set; }
        public List<PropertyReview> ReviewsAboutYourProperty { get; set; }
        public List<Reservation> Reservations { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
