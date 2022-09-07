using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Client.Properties.Commands.Update
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, CreatePropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UpdatePropertyCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<CreatePropertyResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = await _unit.PropertyRepository
                .GetByIdAsync(request.RouteId, null, FileHelpers.AllPropertyRelationIncludes());
            if (property is null) throw new NotFoundException("Property");
            _unit.PropertyRepository.Update(property);
            _mapper.Map(request, property);

            RemoveImages(request, property);
            await CheckAddMainImage(request, property);
            await CheckAddDetailImages(request, property);
            RemoveAmenities(request, property);
            CheckAddPropertyAmenities(request, property);

            await _unit.SaveChangesAsync();
            property = await _unit.PropertyRepository
                .GetByIdAsync(property.Id, null, FileHelpers.AllPropertyRelationIncludes());
            CreatePropertyResponse response = _mapper.Map<CreatePropertyResponse>(property);
            return response;
        }

        private async Task CheckAddMainImage(UpdatePropertyCommand request, Property property)
        {
            var mainImg = property.PropertyImages.FirstOrDefault(x => x.IsMain == true);
            if (mainImg is not null && request.MainPropertyImage is not null)
            {
                if (!request.MainPropertyImage.IsImageOkay(2))
                {
                    throw new PropertyImageValidationException
                    { ErrorMessage = $"{request.MainPropertyImage.FileName} image size too big" };
                }
                //main image
                FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyImages", mainImg.Name);
                mainImg.Name = await request.MainPropertyImage
                    .FileCreate(_env.WebRootPath, "assets/images/PropertyImages");
            }
            else if (mainImg is null && request.MainPropertyImage is not null)
            {
                if (!request.MainPropertyImage.IsImageOkay(2))
                {
                    throw new PropertyImageValidationException
                    { ErrorMessage = $"{request.MainPropertyImage.FileName} image size too big" };
                }
                PropertyImage main = new()
                {
                    Name = await request.MainPropertyImage
          .FileCreate(_env.WebRootPath, "assets/images/PropertyImages"),
                    IsMain = true,
                    Property = property
                };
                property.PropertyImages.Add(main);
            }
            else if (mainImg is null && request.MainPropertyImage is null)
            {
                throw new PropertyImageValidationException
                { ErrorMessage = "You must have 1 main image, please enter main image" };
            }

            //don't do anything if mainImg is not null and req.MainPrpImg is null
        }

        public async Task CheckAddDetailImages(UpdatePropertyCommand request, Property property)
        {
            // detail images
            if (request.DetailPropertyImages != null && request.DetailPropertyImages.Count != 0)
            {
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
                        //Alternative = "Apartment detail image",
                        IsMain = false
                    };
                    property.PropertyImages.Add(detailImages);
                }

            }
            if (!property.PropertyImages.Where(x => x.IsMain == false).Any())
            {
                throw new PropertyImageValidationException
                { ErrorMessage = "You must have at least 1 detail images" };//bunu deyish 4e sonra
            }
        }

        private void CheckAddPropertyAmenities(UpdatePropertyCommand request, Property property)
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
            if (!property.PropertyAmenities.Any())
            {
                throw new PropertyAmenityValidationException
                { ErrorMessage = "You must have at least 1 property amenity" };
            }

        }

        private void RemoveImages(UpdatePropertyCommand request, Property property)
        {
            if (request.DeletedPropertyImages != null && request.DeletedPropertyImages.Count != 0)
            {
                List<Guid> removableImageIds = new();
                request.DeletedPropertyImages.ForEach(imageId =>
                {
                    var propertyImage = property.PropertyImages.FirstOrDefault(x => x.Id == imageId);
                    if(propertyImage is null) throw new PropertyImageValidationException
                        { ErrorMessage = $"Image with this Id({imageId}) doesn't exist" };
                    removableImageIds.Add(imageId);
                    });
                property.PropertyImages.ForEach(propImg =>
                {
                    if (removableImageIds.Any(imgId => imgId == propImg.Id))
                    {
                        FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyImages", propImg.Name);
                    }
                });
                property.PropertyImages.RemoveAll(propImage => removableImageIds
                .Any(remImageId => propImage.Id == remImageId));

            }
        }
        private void RemoveAmenities(UpdatePropertyCommand request, Property property)
        {
            if (request.DeletedPropertyAmenities != null && request.DeletedPropertyAmenities.Count != 0)
            {
                List<Guid> removableAmenityIds = new();
                request.DeletedPropertyAmenities.ForEach(amenityId =>
                {
                    var propertyAmenity= property.PropertyAmenities.FirstOrDefault(x => x.Id == amenityId);
                    if (propertyAmenity is null) throw new PropertyAmenityValidationException
                    { ErrorMessage = $"Amenity with this Id({amenityId}) doesn't exist" };
                    removableAmenityIds.Add(amenityId);
                });
                property.PropertyAmenities.RemoveAll(propAmenity => removableAmenityIds
                .Any(remAmenityId => propAmenity.Id == remAmenityId));

            }
        }
    }
}
