using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.AppUserRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class Property : BaseEntity
    {
        public Property()
        {
            PropertyImages = new();
            PropertyAmenities = new();
            //Reviews=new();
            Reservations = new();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        // price  INT di
        public int? Price { get; set; }

        // bool IsPetAllowed
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public byte? MinNightCount { get; set; }
        public byte? MaxNightCount { get; set; }
        public byte? MaxGuestCount { get; set; }
        public byte? BathroomCount { get; set; }
        public byte? BedroomCount { get; set; }
        public byte? BedCount { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        //hansi saatlar arasi check in ede bilerler
        //public string FromCheckinTime { get; set; }
        //// tocheckintime olmayada biler, belke tek 1 saatda qebul olunur.
        //public string ToCheckinTime { get; set; }
        //public string CheckoutTime { get; set; }
        //relations
        public List<PropertyImage> PropertyImages { get; set; }
        public List<PropertyAmenity> PropertyAmenities { get; set; }
        //public List<PropertyReview> Reviews { get; set; }
        // burda guest reviewda saxlaya bilersen ki, propertye uygun guest reviewlari goturesen.
        public Guid? PropertyGroupId { get; set; }
        public PropertyGroup PropertyGroup { get; set; }
        public Guid? PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public Guid? PrivacyTypeId { get; set; }
        public PrivacyType PrivacyType { get; set; }
        public Guid? AirCoverId { get; set; }
        public AirCover AirCover { get; set; }

        //public List<PropertyHouseRule> PropertyHouseRules { get; set; }
        public Guid? CancellationPolicyId { get; set; }
        public CancellationPolicy CancellationPolicy { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
        public List<Reservation> Reservations { get; set; }

    }
}
