using Airbnb.Application.Contracts.v1.Property.Responses;
using Airbnb.Application.Contracts.v1.Property.Responses.NestedPropertyResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Commands.Create
{
    public class CreatePropertyCommand : IRequest<PropertyResponse>
    {
        public string AppUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public byte MinNightCount { get; set; }
        public byte MaxNightCount { get; set; } = 60; // default 60 - sehifede beledi(max 60 olur)
        public byte MaxGuestCount { get; set; }
        //public byte AdultCount { get; set; }
        //public byte ChildrenCount { get; set; }
        public byte BathroomCount { get; set; }
        public byte BedroomCount { get; set; }
        public byte BedCount { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
       
        //relations
        public List<IFormFile> PropertyImages { get; set; }
        public List<Guid> PropertyAmenities { get; set; }
        public Guid PropertyGroupId { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid PrivacyTypeId { get; set; }
        public Guid AirCoverId { get; set; }
        public Guid CancellationPolicyId { get; set; }
    }
}
