using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Amenities;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Update;
using Airbnb.Domain.Entities.PropertyRelated;
using FluentValidation;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Commands.Update
{
    public class UpdateAmenityCommandValidator:AbstractValidator<UpdateAmenityCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateAmenityCommandValidator(IUnitOfWork unit)
        {

            _unit = unit;
            RuleFor(x => x.Icon).MaximumLength(300);
            RuleFor(x => x.Description).Length(5, 500);

            When(x=>!string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (x, cancellationToken) =>
                {
                    if (await _unit.AmenityRepository.GetSingleAsync(x => x.Name == x.Name) is not null)
                        return false;
                    return true;
                }).WithMessage("Amenity with this name already exists").WithErrorCode("409");
            });
            When(x => x.AmenityTypeId.HasValue, () =>
            {
                RuleFor(x => x.AmenityTypeId)
               .MustAsync(async (x, cancellationToken) =>
               {
                   AmenityType existed = await _unit.AmenityTypeRepository.GetByIdAsync((Guid)x, null);
                   if (existed is null) return false;
                   return true;
               }).WithMessage("Amenity type with this Id doesn't exist.").WithErrorCode("404");
            });
           
        }
    }
}
