using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Client.Properties.Commands.Create
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyResponse>
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
        public async Task<CreatePropertyResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request);
            await CheckAddMainImage(request, property);
            await CheckAddDetailImages(request, property);
            AddPropertyAmenities(request, property);

            await _unit.PropertyRepository.AddAsync(property);
            // bele etmesem Property nin relation classlari null olaraq qalir, gerek deyerlerini burda set etim. Bele daha yaxshidi mence
            property = await _unit.PropertyRepository
                .GetByIdAsync(property.Id, null, FileHelpers.AllPropertyRelationIncludes());
            CreatePropertyResponse response = _mapper.Map<CreatePropertyResponse>(property);
            return response;
        }

        public async Task CheckAddDetailImages(CreatePropertyCommand request, Property property)
        {
            // detail images
            if (request.DetailPropertyImages is null || !request.DetailPropertyImages.Any())
            {
                throw new PropertyImageValidationException
                { ErrorMessage = "You must have at least 1 detail images" };//bunu deyish 4e sonra
            }
            // birinci shekil main shekildi
            foreach (var image in request.DetailPropertyImages)
            {
                if (!image.IsImageOkay(2))
                {
                    throw new PropertyImageValidationException
                    { ErrorMessage = $"{image.FileName} image size too big" };
                }
                PropertyImage detailImages = new()
                {
                    Name = await image
                   .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                    IsMain = false,
                    Property=property
                };
                property.PropertyImages.Add(detailImages);
            }

        }

        private async Task CheckAddMainImage(CreatePropertyCommand request, Property property)
        {
            if (request.MainPropertyImage is null)
            {
                throw new PropertyImageValidationException
                { ErrorMessage = "You must have 1 main image, please enter main image" };
            }
            if (!request.MainPropertyImage.IsImageOkay(2))
            {
                throw new PropertyImageValidationException
                { ErrorMessage = $"{request.MainPropertyImage.FileName} image size too big" };
            }
            property.PropertyImages = new();
            PropertyImage main = new()
            {
                Name = await request.MainPropertyImage
                    .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                IsMain = true,
                Property = property
            };
            property.PropertyImages.Add(main);
        }
      
        private void AddPropertyAmenities(CreatePropertyCommand request, Property property)
        {
            property.PropertyAmenities = new();

            foreach (Guid amenityId in request.PropertyAmenities)
            {
                // Amenity amenity = _unit.AmenityRepoGetById edib add etmek olar. Bunu ele 
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
