using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.Base;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public int PetCount { get; set; }
        // gunlerle biryerde olan pricedi, normalini propertyden chek goster ekranda idc
        public int PricePerDay { get; set; }
        public int ServiceFee { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        // who's reserving
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        // who ownes it
        public Guid HostId { get; set; }
        public Host Host { get; set; }
        // one to one
        public PropertyReview PropertyReview { get; set; }
        public GuestReview GuestReview { get; set; }
    }
}
