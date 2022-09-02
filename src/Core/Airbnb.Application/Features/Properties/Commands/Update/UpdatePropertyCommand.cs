﻿using Airbnb.Application.Contracts.v1.Property.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Commands.Update
{
    public class UpdatePropertyCommand:IRequest<PropertyResponse>
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
        //public byte? AdultCount { get; set; }
        //public byte? ChildrenCount { get; set; }
        public int? BathroomCount { get; set; }
        public int? BedroomCount { get; set; }
        public int? BedCount { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        //relations
        public List<IFormFile> PropertyImages { get; set; }
        public List<Guid> PropertyAmenities { get; set; }
        public Guid? PropertyGroupId { get; set; }
        public Guid? PropertyTypeId { get; set; }
        public Guid? PrivacyTypeId { get; set; }
        public Guid? AirCoverId { get; set; }
        public Guid? CancellationPolicyId { get; set; }
    }
}