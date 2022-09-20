using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Properties.Commands.Create
{
    public class CreatePropertyCommand : IRequest<CreatePropertyResponse>
    {
        public Guid HostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int MinNightCount { get; set; } = 1;
        public int MaxNightCount { get; set; } = 60; // default 60 - sehifede beledi(max 60 olur)
        public int MaxGuestCount { get; set; }
        public int BathroomCount { get; set; }
        public int BedroomCount { get; set; }
        public int BedCount { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }

        //relations
        public IFormFile MainPropertyImage { get; set; }
        public List<IFormFile> DetailPropertyImages { get; set; }
        public List<IFormFile> BedImages { get; set; }
        public List<Guid> PropertyAmenities { get; set; }
        public Guid PropertyGroupId { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid PrivacyTypeId { get; set; }
        public Guid AirCoverId { get; set; }
        public Guid CancellationPolicyId { get; set; }
      
    }
}
