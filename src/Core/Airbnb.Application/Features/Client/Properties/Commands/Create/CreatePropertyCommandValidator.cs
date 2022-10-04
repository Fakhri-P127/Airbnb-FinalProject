using Airbnb.Application.Common.Interfaces;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using FluentValidation;

namespace Airbnb.Application.Features.Client.Properties.Commands.Create
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        private readonly IUnitOfWork _unit;
        private Region _region;
        private Country _country;
        private City _city;
        public CreatePropertyCommandValidator(IUnitOfWork unit)
        {
            _unit = unit;

            RuleFor(x => x.Price).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100000).NotEmpty();

            RuleFor(x => x.MinNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.MaxNightCount).GreaterThanOrEqualTo(x => x.MinNightCount).LessThanOrEqualTo(60);
            RuleFor(x => x.MaxGuestCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();
            RuleFor(x => x.MaxNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.BathroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();
            RuleFor(x => x.BedroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();
            RuleFor(x => x.BedCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();

            RuleFor(x => x.Title).Length(3, 100).NotEmpty();
            RuleFor(x => x.Description).Length(5, 1000).NotEmpty();
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.Longitude).NotEmpty();

            RuleFor(x => x.Street).Length(3, 60).NotEmpty();

            RuleFor(x => x.MainPropertyImage).NotEmpty();
            RuleForEach(x => x.DetailPropertyImages).NotEmpty();
            //RuleFor(x => x.PropertyAmenities).NotEmpty();
            RuleForEach(x => x.PropertyAmenities).NotEmpty();


            RuleFor(x => x.CheckInTime).GreaterThanOrEqualTo(new TimeSpan(0, 0, 0))
               .LessThanOrEqualTo(new TimeSpan(23, 59, 59));

            RuleFor(x => x.CheckOutTime)
               .GreaterThan(new TimeSpan(0, 0, 0))
               .LessThanOrEqualTo(new TimeSpan(23, 59, 59));


            RuleFor(x => x.AirCoverId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                bool exists = await _unit.AirCoverRepository.GetByIdAsync(id, null) is not null;
                return exists;
            }).WithMessage("Aircover with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.HostId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                bool exists = await _unit.HostRepository.GetByIdAsync(id, null) is not null;
                return exists;
            }).WithMessage("Host with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.CancellationPolicyId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                bool exists = await _unit.CancellationPolicyRepository.GetByIdAsync(id, null) is not null;
                return exists;
            }).WithMessage("Cancellation policy with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.PrivacyTypeId).NotEmpty().MustAsync(async (id, cancellationToken) =>
           {
               bool exists = await _unit.PrivacyTypeRepository.GetByIdAsync(id, null) is not null;
               return exists;
           }).WithMessage("Privacy type with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.PropertyGroupId).NotEmpty().MustAsync(async (id, cancellationToken) =>
           {
               bool exists = await _unit.PropertyGroupRepository.GetByIdAsync(id, null) is not null;
               return exists;
           }).WithMessage("Property group with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.PropertyTypeId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                bool exists = await _unit.PropertyTypeRepository.GetByIdAsync(id, null) is not null;
                return exists;
            }).WithMessage("Property type with this Id doesn't exist").WithErrorCode("404");


            RuleFor(x => x.RegionId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                Region exists = await _unit.RegionRepository.GetByIdAsync(id, null,
                    false, "Countries");
                if (exists is null) return false;
                _region = exists;
                return true;
            }).WithMessage("Region with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.CountryId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                Country exists = await _unit.CountryRepository.GetByIdAsync(id, null,
                    false, "Cities");
                if (exists is null) return false;
                _country = exists;
                return true;
            }).WithMessage("Country with this Id doesn't exist").WithErrorCode("404");

            RuleFor(x => x.CityId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                City exists = await _unit.CityRepository.GetByIdAsync(id, null);
                if (exists is null) return false;
                _city = exists;
                return true;
            }).WithMessage("City with this Id doesn't exist").WithErrorCode("404");


            if (_region is not null && _country is not null)
            {
                RuleFor(x => x.CountryId)
                    .MustAsync(async (x, cancellationToken) =>
                    {
                        return await Task.FromResult(_region.Countries.FirstOrDefault(c => c.Id == _country.Id) is null);
                    }).WithMessage($"{_country.Name} doesn't belong to {_region.Name} region. Please choose your region or country correctly.");
            }

            if (_city is not null && _country is not null)
            {
                RuleFor(x => x.CityId)
                 .MustAsync(async (x, cancellationToken) =>
                 {
                     return await Task.FromResult(_country.Cities.FirstOrDefault(c => c.Id == _city.Id) is null);
                 }).WithMessage($"{_city.Name}(city) doesn't belong to {_country.Name}(country). Please choose your country or city correctly.");
            }
        }
    }
}



