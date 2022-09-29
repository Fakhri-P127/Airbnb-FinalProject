using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Airbnb.Domain.Entities.PropertyRelated;

namespace Airbnb.Application.Contracts.v1.Client.Property.Parameters
{
    public class PropertyParameters:BaseQueryStringParameters
    {
        public PropertyParameters()
        {
            Amenities = new();
        }
        public string Title { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? IsPetAllowed { get; set; }
        //public Guid StateId { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public int? MinMinimumNightCount{ get; set; }
        public int? MaxMinimumNightCount { get; set; }
        public int? MinMaximumNightCount { get; set; }
        public int? MaxMaximumNightCount { get; set; }
        public int? MinGuestCountLimit { get; set; }
        public int? MaxGuestCountLimit { get; set; }
        public int? MinBathroomCount { get; set; }
        public int? MaxBathroomCount { get; set; }
        public int? MinBedroomCount { get; set; }
        public int? MaxBedroomCount { get; set; }
        public int? MinBedCount { get; set; }
        public int? MaxBedCount { get; set; }
        //public decimal? Latitude { get; set; }
        //public decimal? Longitude { get; set; }

        public TimeSpan? MinCheckInTime { get; set; }
        public TimeSpan? MaxCheckInTime { get; set; }
        public TimeSpan? MinCheckOutTime { get; set; }
        public TimeSpan? MaxCheckOutTime { get; set; }

        //relations
        public List<Guid> Amenities { get; set; }
        public float? MinOverallScore { get; set; }
        public float? MaxOverallScore { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public Guid? PropertyTypeId { get; set; }
        public Guid? PrivacyTypeId { get; set; }
        public Guid? AirCoverId { get; set; }
        public Guid? CancellationPolicyId { get; set; }
        public Guid? HostId { get; set; }
    }
}
