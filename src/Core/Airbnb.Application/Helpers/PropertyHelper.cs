using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Features.Client.Properties.Commands.Update;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Helpers
{
    public static class PropertyHelper
    {
        public async static Task<CreatePropertyResponse> ReturnResponse(Property property,
          IUnitOfWork _unit, IMapper _mapper)
        {
            property = await _unit.PropertyRepository.GetByIdAsync(property.Id, null,
                AllPropertyIncludes());
            CreatePropertyResponse response = _mapper.Map<CreatePropertyResponse>(property);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllPropertyIncludes()
        {
            string[] includes = new[] { "PropertyImages"
                , "PropertyAmenities", "PropertyAmenities.Amenity", "PropertyGroup", "PropertyType", "AirCover"
                , "CancellationPolicy", "PrivacyType","Host","Reservations","Host.AppUser" };

            return includes;
        }
        public static void CheckImageIsOkay(IFormFile image)
        {
            if (!image.IsImageOkay(2)) throw new PropertyImageValidationException
            { ErrorMessage = $"{image.FileName} image size too big" };
        }
        public static async Task CreateMainImage(IFormFile MainPropertyImage, Property property,
            IWebHostEnvironment _env)
        {
            PropertyImage main = new()
            {
                Name = await MainPropertyImage
      .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                IsMain = true,
                Alternative = "Main image",
                Property = property
            };
            property.PropertyImages.Add(main);
        }
        public static async Task CreateDetailImage(Property property, IFormFile image, IWebHostEnvironment _env)
        {
            PropertyImage detailImages = new()
            {
                Name = await image
               .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                Alternative = "Apartment detail image",
                IsMain = false,
                Property = property
            };
            property.PropertyImages.Add(detailImages);
        }
        public static async Task CreateBedImage(Property property, IFormFile image, byte counter,
            IWebHostEnvironment _env)
        {
            PropertyImage bedImages = new()
            {
                Name = await image
               .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                Alternative = $"Bedroom {counter}",
                IsMain = null,
                Property = property
            };
            property.PropertyImages.Add(bedImages);
        }
    }
}
