using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Property.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Property;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Properties.Commands.Create
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, PropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CreatePropertyCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<PropertyResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request);
            await ImageCheck(request, property);
            AddPropertyAmenities(request, property);
            await _unit.PropertyRepository.AddAsync(property);
            // bele etmesem Property nin relation classlari null olaraq qalir, gerek deyerlerini burda set etim. Bele daha yaxshidi mence
            property = await _unit.PropertyRepository
                .GetByIdAsync(property.Id, null, FileHelpers.AllPropertyRelationIncludes());
            PropertyResponse response = _mapper.Map<PropertyResponse>(property);
            return response;
        }

        private async Task ImageCheck(CreatePropertyCommand request, Property property)
        {
            property.PropertyImages = new();
            if (request.PropertyImages.Count != 0)
            {
                // birinci shekil main shekildi
                IFormFile main = request.PropertyImages.FirstOrDefault();
                foreach (IFormFile image in request.PropertyImages)
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
                            //Alternative = "Apartment main image",
                            IsMain = true
                        };
                        property.PropertyImages.Add(mainImage);
                        continue;
                    }
                    PropertyImage detailImages = new()
                    {
                        Name = await image
                       .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                        //Alternative = "Apartment detail image",
                        IsMain = false
                    };
                    property.PropertyImages.Add(detailImages);
                }

            }
        }
        private void AddPropertyAmenities(CreatePropertyCommand request, Property property)
        {
            property.PropertyAmenities = new();

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
