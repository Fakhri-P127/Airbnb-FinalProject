using Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedResponses
{
    public class PropertyInGetHostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        //public string Description { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        //hansi saatlar arasi check in ede bilerler
        //public string FromCheckinTime { get; set; }
        //// tocheckintime olmayada biler, belke tek 1 saatda qebul olunur.
        //public string ToCheckinTime { get; set; }
        //public string CheckoutTime { get; set; }

        // status u burdan yigishdirib, get all edende ele statusu true olanlari cagir.

        //relations
        //public List<PropertyImagesInPropertyResponse> PropertyImages { get; set; }
        //public List<PropertyAmenitiesInPropertyResponse> PropertyAmenities { get; set; }
        public PropertyGroupInPropertyResponse PropertyGroup { get; set; }
        public PropertyTypeInPropertyResponse PropertyType { get; set; }
        public PrivacyTypeInPropertyResponse PrivacyType { get; set; }
        //public AirCoverInPropertyResponse AirCover { get; set; }
        public CancellationPolicyInPropertyResponse CancellationPolicy { get; set; }
        //public List<PropertyReviewInReservationPropertyResponse> Reviews { get; set; }
        //public List<ReservationInPropertyResponse> Reservations { get; set; }
    }
}
