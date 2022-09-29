using Airbnb.Application.Common.Interfaces;
using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Client.Property.Parameters
{
    public class PropertyParametersValidator:AbstractValidator<PropertyParameters>
    {
        private readonly IUnitOfWork _unit;

        public PropertyParametersValidator(IUnitOfWork unit)
        {
            _unit = unit;

            RuleFor(x => x.Title).Length(3, 100);

            RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100000);
            RuleFor(x => x.MaxPrice).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100000);
            When(x => x.MinPrice.HasValue, () =>
            {
                RuleFor(x => x.MaxPrice).GreaterThan(x => x.MinPrice);
            });

            RuleFor(x => x.MinOverallScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            RuleFor(x => x.MaxOverallScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            When(x => x.MinOverallScore.HasValue, () => 
            {
                RuleFor(x => x.MaxOverallScore).GreaterThan(x => x.MinOverallScore);
            });

            //RuleFor(x=>x.IsPetAllowed)
            RuleFor(x => x.MinGuestCountLimit).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.MaxGuestCountLimit).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            When(x => x.MinGuestCountLimit.HasValue, () =>
            {
                RuleFor(x => x.MaxGuestCountLimit).GreaterThan(x => x.MinGuestCountLimit);
            });

            RuleFor(x => x.MinBathroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.MaxBathroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            When(x => x.MinBathroomCount.HasValue, () =>
            {
                RuleFor(x => x.MaxBathroomCount).GreaterThan(x => x.MinBathroomCount);
            });

            RuleFor(x => x.MinBedroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.MaxBedroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            When(x => x.MinBedroomCount.HasValue, () =>
            {
                RuleFor(x => x.MaxBedroomCount).GreaterThan(x => x.MinBedroomCount);
            });

            RuleFor(x => x.MinBedCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.MaxBedCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            When(x => x.MinBedCount.HasValue, () =>
            {
                RuleFor(x => x.MaxBedCount).GreaterThan(x => x.MinBedCount);
            });

            RuleForEach(x => x.Amenities).MustAsync(async (x, cancellationToken) =>
            {
                bool exists = await _unit.AmenityRepository.GetByIdAsync(x, null) is not null;
                return true;
            }).WithMessage("Amenity with this id doesn't exist");

            When(x => x.RegionId.HasValue, () =>
            {
                RuleFor(x => x.RegionId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.RegionRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Region with this Id doesn't exist");
            });

            When(x => x.CountryId.HasValue, () =>
            {
                RuleFor(x => x.CountryId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.CountryRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Country with this Id doesn't exist");
            });

            When(x => x.CityId.HasValue, () =>
            {
                RuleFor(x => x.CityId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.CityRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("City with this Id doesn't exist");
            });

            When(x => x.HostId.HasValue, () =>
            {
                RuleFor(x => x.HostId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.HostRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Host with this Id doesn't exist");
            });

            When(x => x.AirCoverId.HasValue, () =>
            {
                RuleFor(x => x.AirCoverId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.AirCoverRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Aircover with this Id doesn't exist");
            });
            
            When(x => x.CancellationPolicyId.HasValue, () =>
            {
                RuleFor(x => x.CancellationPolicyId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.CancellationPolicyRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Cancellation policy with this Id doesn't exist");
            });

            When(x => x.PrivacyTypeId.HasValue, () =>
            {
                RuleFor(x => x.PrivacyTypeId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.PrivacyTypeRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Privacy type with this Id doesn't exist");
            });

            When(x => x.PropertyGroupId.HasValue, () =>
            {
                RuleFor(x => x.PropertyGroupId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.PropertyGroupRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Property group with this Id doesn't exist");
            });

            When(x => x.PropertyTypeId.HasValue, () =>
            {
                RuleFor(x => x.PropertyTypeId).MustAsync(async (x, cancellationToken) =>
                {
                    bool exists = await _unit.PropertyTypeRepository.GetByIdAsync(x.Value, null) is not null;
                    return exists;
                }).WithMessage("Property type with this Id doesn't exist");
            });

            #region default checkInAndOutDates
            RuleFor(x => x.MinCheckInTime).GreaterThanOrEqualTo(new TimeSpan(0,0,0))
                .LessThanOrEqualTo(new TimeSpan(23,59,59));

            RuleFor(x => x.MaxCheckInTime)
               .GreaterThan(new TimeSpan(0, 0, 0))
               .LessThanOrEqualTo(new TimeSpan(23, 59, 59));

            RuleFor(x => x.MinCheckOutTime)
               .GreaterThanOrEqualTo(new TimeSpan(0, 0, 0))
               .LessThanOrEqualTo(new TimeSpan(23, 59, 59));

            RuleFor(x => x.MaxCheckOutTime)
                .GreaterThanOrEqualTo(new TimeSpan(0, 0, 0))
                .LessThanOrEqualTo(new TimeSpan(23, 59, 59));
            #endregion
            #region condtional validation for checkIn and checkOut dates
            When(x => x.MinCheckInTime.HasValue, () =>
            {
                RuleFor(x => x.MaxCheckInTime)
                .GreaterThanOrEqualTo(x => x.MinCheckInTime.Value.Add(TimeSpan.FromHours(1)));

                //RuleFor(x => x.MinCheckOutTime)
                //    .GreaterThanOrEqualTo(x => x.MinCheckInTime.Value.Add(TimeSpan.FromHours(1)));
                ////.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));

                //RuleFor(x => x.MaxCheckOutTime)
                //    .GreaterThanOrEqualTo(x => x.MinCheckInTime.Value.Add(TimeSpan.FromHours(1)));
                //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));
            });
            //When(x => x.MaxCheckInTime.HasValue, () =>
            //{
            //    RuleFor(x => x.MinCheckOutTime)
            //  .LessThanOrEqualTo(x => x.MaxCheckInTime.Value.Date.AddDays(1));

            //    RuleFor(x => x.MaxCheckOutTime)
            //        .GreaterThanOrEqualTo(x => x.MaxCheckInTime.Value.Date.AddDays(1));
            //    //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));
            //});
            When(x => x.MinCheckOutTime.HasValue, () =>
            {
                RuleFor(x => x.MaxCheckOutTime)
                    .GreaterThanOrEqualTo(x => x.MinCheckOutTime.Value.Add(TimeSpan.FromHours(1)));
                //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));

                // tam deqiqlik uchun bu tipde yazmaq olar amma mence ehtiyac yoxdu
                //RuleFor(x => x.MaxCheckInTime).LessThanOrEqualTo(x => x.MinCheckOutTime.Value.AddDays(-1));
            });
            #endregion


            RuleFor(x => x.MinMinimumNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.MaxMinimumNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.MinMaximumNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.MaxMaximumNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);

            When(x => x.MinMinimumNightCount.HasValue, () =>
            {
                RuleFor(x => x.MaxMinimumNightCount).GreaterThan(x => x.MinMinimumNightCount);
                RuleFor(x => x.MaxMinimumNightCount).GreaterThanOrEqualTo(x => x.MinMinimumNightCount.Value);
                RuleFor(x => x.MaxMaximumNightCount).GreaterThanOrEqualTo(x => x.MinMinimumNightCount.Value);
            });
            When(x => x.MaxMinimumNightCount.HasValue, () =>
            {
                //RuleFor(x => x.MaxMinimumNightCount).LessThanOrEqualTo(x => x.MaxMinimumNightCount.Value);

                RuleFor(x => x.MaxMaximumNightCount).GreaterThanOrEqualTo(x => x.MaxMinimumNightCount.Value);
            });

            When(x => x.MinMaximumNightCount.HasValue, () =>
            {
                RuleFor(x => x.MaxMaximumNightCount).GreaterThan(x => x.MinMaximumNightCount);
            });
        }
    }
}
