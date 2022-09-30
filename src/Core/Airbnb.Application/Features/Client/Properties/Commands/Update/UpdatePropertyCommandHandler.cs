using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Properties.Commands.Update
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, CreatePropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _accessor;

        public UpdatePropertyCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env,
            IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
            _accessor = accessor;
        }
        public async Task<CreatePropertyResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Property property = await _unit.PropertyRepository
                .GetByIdAsync(Id, null,true, PropertyHelper.AllPropertyIncludes());
            if (property is null) throw new PropertyNotFoundException();
            _unit.PropertyRepository.Update(property);
            _mapper.Map(request, property);
            await SetStateForProperty(request, property);
            RemoveImages(request, property);
            await CheckAddMainImage(request, property);
            await CheckAddDetailImages(request, property);
            await CheckAddBedImages(request, property);
            RemoveAmenities(request, property);
            CheckAddPropertyAmenities(request, property);

            await _unit.SaveChangesAsync();
            return await PropertyHelper.ReturnResponse(property, _unit, _mapper);

        }
        private async Task SetStateForProperty(UpdatePropertyCommand request, Property property)
        {
            // eger hamisi nulldisa neyise deyishmeye ehtiyac yoxdu
            if (request.RegionId is null && request.CountryId is null
                && request.CityId is null && request.Street is null) return;
            //eger hansisa deyeri deyishmirikse, null gelibse Property.State deki deyere beraber edek.
            request.RegionId ??= property.State.RegionId;
            request.CountryId ??= property.State.CountryId;
            request.CityId ??= property.State.CityId;
            request.Street ??= property.State.Street;
            State existedState = await _unit.StateRepository
                .GetSingleAsync(x => x.RegionId == request.RegionId
                && x.CountryId == request.CountryId && x.CityId == request.CityId
                && x.Street == request.Street.Trim().ToLower());
            if (existedState is null)
            {
                await CheckForStateExceptions(request);
                property.State = new()
                {
                    RegionId = (Guid)request.RegionId,
                    CountryId = (Guid)request.CountryId,
                    CityId = (Guid)request.CityId,
                    Street = request.Street.Trim().ToLower(),
                };
            }
            else
            {
                property.StateId = existedState.Id;
            }
        }
        //fluent validationda duzeltdiyivi yoxla

        private async Task CheckForStateExceptions(UpdatePropertyCommand request)
        {
            //Region region = await _unit.RegionRepository.GetByIdAsync((Guid)request.RegionId, null, false,
            //    "Countries");
            //Country country = await _unit.CountryRepository.GetByIdAsync((Guid)request.CountryId, null, false,
            //    "Cities");
            Region region = await _unit.RegionRepository.GetByIdAsync((Guid)request.RegionId, null);
            Country country = await _unit.CountryRepository.GetByIdAsync((Guid)request.CountryId, null);
            City city = await _unit.CityRepository.GetByIdAsync((Guid)request.CityId, null);
            if (country.RegionId != request.RegionId) 
                throw new CountryDoesntBelongToSpecificiedRegionException(country.Name,region.Name);
            if(city.CountryId != country.Id)
                throw new CityDoesntBelongToSpecificiedCountryException(city.Name, country.Name);
            //if (region.Countries.FirstOrDefault(c => c.Id == country.Id) is null)
            //    throw new CountryDoesntBelongToSpecificiedRegionException(country.Name, region.Name);
            //if (country.Cities.FirstOrDefault(c => c.Id == request.CityId) is null)
            //    throw new CityDoesntBelongToSpecificiedCountryException(city.Name, country.Name);
        }

        private async Task CheckAddMainImage(UpdatePropertyCommand request, Property property)
        {
            var mainImg = property.PropertyImages.FirstOrDefault(x => x.IsMain == true);
            if (mainImg is not null && request.MainPropertyImage is not null)
            {
                PropertyHelper.CheckImageIsOkay(request.MainPropertyImage);
                //main image
                FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyImages", mainImg.Name);
                mainImg.Name = await request.MainPropertyImage
                    .FileCreate(_env.WebRootPath, "assets/images/PropertyImages");
            }
            else if (mainImg is null && request.MainPropertyImage is not null)
            {
                PropertyHelper.CheckImageIsOkay(request.MainPropertyImage);
                await PropertyHelper.CreateMainImage(request.MainPropertyImage, property,_env);
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
                foreach (IFormFile image in request.DetailPropertyImages)
                {
                    PropertyHelper.CheckImageIsOkay(image);
                    await PropertyHelper.CreateDetailImage(property, image,_env);
                }
            }
            if (!property.PropertyImages.Where(x => x.IsMain == false).Any())   
                throw new PropertyImageValidationException
                { ErrorMessage = "You must have at least 1 detail image" };//bunu deyish 4e sonra
        }

        public async Task CheckAddBedImages(UpdatePropertyCommand request, Property property)
        {
            // bed images
            if (request.BedImages != null && request.BedImages.Count != 0)
            {
                byte counter = 1;
                foreach (IFormFile image in request.BedImages)
                {
                    PropertyHelper.CheckImageIsOkay(image);

                    await PropertyHelper.CreateBedImage(property, image, counter,_env);
                    counter++;
                }
            }
            //if (!property.PropertyImages.Where(x => x.IsMain == null).Any())
            //{
            //    throw new PropertyImageValidationException
            //    { ErrorMessage = "You must have at least 1 bed image" };
            //}
        }

        private static void CheckAddPropertyAmenities(UpdatePropertyCommand request, Property property)
        {
            if (request.PropertyAmenities != null && request.PropertyAmenities.Count != 0)
            {
                foreach (Guid amenityId in request.PropertyAmenities.Distinct())
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
                    if (propertyImage is null) throw new PropertyImageValidationException
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
        private static void RemoveAmenities(UpdatePropertyCommand request, Property property)
        {
            if (request.DeletedPropertyAmenities != null && request.DeletedPropertyAmenities.Count != 0)
            {
                List<Guid> removableAmenityIds = new();
                request.DeletedPropertyAmenities.ForEach(amenityId =>
                {
                    var propertyAmenity = property.PropertyAmenities.FirstOrDefault(x => x.Id == amenityId);
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
