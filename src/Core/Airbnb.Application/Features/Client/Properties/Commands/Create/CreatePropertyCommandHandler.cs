using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

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
            Host host = await CheckIfNotFoundThenReturnHost(request);
            Property property = _mapper.Map<Property>(request);
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
        private async Task<Host> CheckIfNotFoundThenReturnHost(CreatePropertyCommand request)
        {
            Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null,"AppUser");
            if (host is null) throw new HostNotFoundException(request.HostId);
            return host;
        }
        public async Task CheckAddBedImages(CreatePropertyCommand request,Property property)
        {
            if(request.BedImages is not null)
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
            if (request.MainPropertyImage is null)  throw new PropertyImageValidationException
                { ErrorMessage = "You must have 1 main image, please enter main image" };
            PropertyHelper.CheckImageIsOkay(request.MainPropertyImage);

            await PropertyHelper.CreateMainImage(request.MainPropertyImage, property, _env);
        }
      
        private static void AddPropertyAmenities(CreatePropertyCommand request, Property property)
        {
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
