using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Properties.Commands.Update
{
    public class UpdatePropertyCommand:IRequest<CreatePropertyResponse>
    {
        public Guid RouteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? MinNightCount { get; set; }
        public int? MaxNightCount { get; set; }
        public int? MaxGuestCount { get; set; }
        public int? BathroomCount { get; set; }
        public int? BedroomCount { get; set; }
        public int? BedCount { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        //relations
        public IFormFile MainPropertyImage { get; set; }
        public List<IFormFile> DetailPropertyImages { get; set; }
        public List<Guid> DeletedPropertyImages { get; set; }
        public List<Guid> PropertyAmenities { get; set; }
        public List<Guid> DeletedPropertyAmenities { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public Guid? PropertyTypeId { get; set; }
        public Guid? PrivacyTypeId { get; set; }
        public Guid? AirCoverId { get; set; }
        public Guid? CancellationPolicyId { get; set; }
    }
}
