using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Property.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Features.Properties.Commands.Create;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Property;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Commands.Update
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UpdatePropertyCommandHandler(IUnitOfWork unit,IMapper mapper,IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<PropertyResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = await _unit.PropertyRepository
                .GetByIdAsync(request.RouteId, null, FileHelpers.AllPropertyRelationIncludes());
            if (property is null) throw new PropertyNotFoundException();
            _unit.PropertyRepository.Update(property);
            _mapper.Map(request, property);

            //property.Title = request.Title;
            //property.Description = request.Description;
    
            await ImageCheck(request, property);
            AddPropertyAmenities(request, property);
       
            await _unit.SaveChangesAsync();
            property = await _unit.PropertyRepository
                .GetByIdAsync(property.Id, null, FileHelpers.AllPropertyRelationIncludes());
            PropertyResponse response = _mapper.Map<PropertyResponse>(property);
            return response;
        }
        public async Task ImageCheck(UpdatePropertyCommand request, Property property)
        {

            //property.PropertyImages.ForEach(image => FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyImages", image.Name));
            if (request.PropertyImages != null && request.PropertyImages.Count != 0)
            {
                // birinci shekil main shekildi
                IFormFile main = request.PropertyImages.FirstOrDefault();
                foreach (var image in request.PropertyImages)
                {
                    if (!image.IsImageOkay(2))
                    {
                        throw new UserValidationException { ErrorMessage = "Image size too big" };
                    }
                    //if (!string.IsNullOrWhiteSpace(property.PropertyImages))
                    //    FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
                    if (image == main)
                    {
                        PropertyImage mainImage = new()
                        {
                            Name = await image
                        .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                            Alternative = "Apartment main image",
                            IsMain = true
                        };
                        property.PropertyImages.Add(mainImage);
                        continue;
                    }
                    PropertyImage detailImages = new()
                    {
                        Name = await image
                       .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                        Alternative = "Apartment detail image",
                        IsMain = false
                    };
                    property.PropertyImages.Add(detailImages);
                }

            }
        }
        private void AddPropertyAmenities(UpdatePropertyCommand request, Property property)
        {
            //property.PropertyAmenities = new();

            if (request.PropertyAmenities != null && request.PropertyAmenities.Count != 0)
            {
                foreach (Guid amenityId in request.PropertyAmenities)
                {
                    // Amenity amenity = _unit.AmenityRepoGetById edib add etmek olar
                    PropertyAmenity propertyAmenity = new()
                    {
                        AmenityId = amenityId,
                        Property = property,
                    };
                    property.PropertyAmenities.Add(propertyAmenity);
                }
            }
           
           
        }
    }
}
