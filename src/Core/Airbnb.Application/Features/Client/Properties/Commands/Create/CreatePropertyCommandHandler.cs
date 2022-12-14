using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.AirCovers;
using Airbnb.Application.Exceptions.CancellationPolicies;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Exceptions.PropertyGroups;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Properties.Commands.Create
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _acccessor;

        public CreatePropertyCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env,
            IHttpContextAccessor acccessor)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
            _acccessor = acccessor;
        }
        public async Task<CreatePropertyResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            //Host host = await CheckExceptionsThenReturnHost(request);
            //Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null, false, "AppUser");
            Guid userId = _acccessor.HttpContext.User.GetUserIdFromClaim().TryParseStringIdToGuid();
            Host host = await _unit.HostRepository.GetSingleAsync(x => x.AppUserId == userId, true, "AppUser");
            Property property = _mapper.Map<Property>(request);
            property.HostId = host.Id;
            await SetStateForProperty(request, property);
            await CheckAddMainImage(request, property);
            await CheckAddDetailImages(request, property);
            await CheckAddBedImages(request, property);
            AddPropertyAmenities(request, property);
            // null oldugda pending kimi qalir property. Confirmed deyeri varsa onda true
            if (!host.AppUser.EmailConfirmed && !host.AppUser.PhoneNumberConfirmed)
                property.IsDisplayed = null;

            await _unit.PropertyRepository.AddAsync(property);
            // bele etmesem Property nin relation classlari null olaraq qalir, gerek deyerlerini burda set etim. Bele daha yaxshidi mence
            return await PropertyHelper.ReturnResponse(property, _unit, _mapper);
        }

        private async Task SetStateForProperty(CreatePropertyCommand request, Property property)
        {

            State existedState = await _unit.StateRepository
                .GetSingleAsync(x => x.RegionId == request.RegionId
                && x.CountryId == request.CountryId && x.CityId == request.CityId 
                && x.Street == request.Street.Trim().ToLower());
            if (existedState is null)
            {
                property.State = new()
                {
                    RegionId = request.RegionId,
                    CountryId = request.CountryId,
                    CityId = request.CityId,
                    Street = request.Street.Trim().ToLower(),
                };
                //property.State = state;
            }
            else
            {
                property.StateId = existedState.Id;
            }
        }
        public async Task CheckAddBedImages(CreatePropertyCommand request, Property property)
        {
            if (request.BedImages is not null)
            {
                byte counter = 1;
                foreach (IFormFile image in request.BedImages)
                {
                    PropertyHelper.CheckImageIsOkay(image);
                    await PropertyHelper.CreateBedImage(property, image, counter, _env);
                    counter++;
                }
            }
        }
        public async Task CheckAddDetailImages(CreatePropertyCommand request, Property property)
        {
            // detail images
            if (request.DetailPropertyImages is null || !request.DetailPropertyImages.Any())
            {
                throw new PropertyImageValidationException
                { ErrorMessage = "You must have at least 1 detail image" };//bunu deyish 4e sonra
            }
            // birinci shekil main shekildi
            foreach (var image in request.DetailPropertyImages)
            {
                PropertyHelper.CheckImageIsOkay(image);
                await PropertyHelper.CreateDetailImage(property, image, _env);
            }
        }

        private async Task CheckAddMainImage(CreatePropertyCommand request, Property property)
        {
            if (request.MainPropertyImage is null) throw new PropertyImageValidationException
            { ErrorMessage = "You must have 1 main image, please enter main image" };
            PropertyHelper.CheckImageIsOkay(request.MainPropertyImage);

            await PropertyHelper.CreateMainImage(request.MainPropertyImage, property, _env);
        }

        private static void AddPropertyAmenities(CreatePropertyCommand request, Property property)
        {
            //request.PropertyAmenities = .ToList();

            foreach (Guid amenityId in request.PropertyAmenities.Distinct())
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

        #region old way of checking notfounds 
        //private async Task<Host> CheckExceptionsThenReturnHost(CreatePropertyCommand request)
        //{
        //    //Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null, false, "AppUser");
        //    //if (host is null) throw new HostNotFoundException(request.HostId);
        //    //AirCover airCover = await _unit.AirCoverRepository.GetByIdAsync(request.AirCoverId, null);
        //    //if (airCover is null) throw new AirCoverNotFoundException();
        //    //CancellationPolicy cancellationPolicy = await _unit.CancellationPolicyRepository.GetByIdAsync(request.CancellationPolicyId, null);
        //    //if (cancellationPolicy is null) throw new CancellationPolicyNotFoundException();
        //    //PrivacyType privacyType = await _unit.PrivacyTypeRepository.GetByIdAsync(request.PrivacyTypeId, null);
        //    //if (privacyType is null) throw new PrivacyTypeNotFoundException();
        //    //PropertyGroup propertyGroup = await _unit.PropertyGroupRepository.GetByIdAsync(request.PropertyGroupId, null);
        //    //if (propertyGroup is null) throw new PropertyGroupNotFoundException();
        //    //PropertyType propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(request.PropertyTypeId, null);
        //    //if (propertyType is null) throw new PropertyTypeNotFoundException();
        //    //Region region = await _unit.RegionRepository.GetByIdAsync(request.RegionId, null);
        //    //if (region is null) throw new RegionNotFoundException();
        //    //Country country = await _unit.CountryRepository.GetByIdAsync(request.CountryId, null);
        //    //if (country is null) throw new CountryNotFoundException();
        //    //City city = await _unit.CityRepository.GetByIdAsync(request.CityId, null);
        //    //if (city is null) throw new CityNotFoundException();
        //    //if (region.Countries.FirstOrDefault(c => c.Id == country.Id) is null)
        //    //    throw new CountryDoesntBelongToSpecificiedRegionException(country.Name,region.Name);
        //    //if (country.Cities.FirstOrDefault(c => c.Id == city.Id) is null)
        //    //    throw new CityDoesntBelongToSpecificiedCountryException(city.Name,country.Name);
        //    return host;
        //}
        #endregion
    }
}
