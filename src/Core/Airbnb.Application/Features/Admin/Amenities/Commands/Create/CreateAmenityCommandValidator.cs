using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Commands.Create
{
    public class CreateAmenityCommandValidator:AbstractValidator<CreateAmenityCommand>
    {
        private readonly IUnitOfWork _unit;

        public CreateAmenityCommandValidator(IUnitOfWork unit)
        {
            
            _unit = unit;
            RuleFor(x => x.Icon).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Description).NotEmpty().Length(5, 500);
           
            RuleFor(x => x.Name).NotEmpty().MustAsync(async (x, cancellationToken) =>
            {
                if (await _unit.AmenityRepository.GetSingleAsync(x => x.Name == x.Name) is not null)
                    return false;
                return true;
            }).WithMessage("Amenity with this name already exists").WithErrorCode("409");

            RuleFor(x => x.AmenityTypeId).NotEmpty()
                .MustAsync(async (x, cancellationToken) =>
                {
                    AmenityType existed = await _unit.AmenityTypeRepository.GetByIdAsync(x,null);
                    if (existed is null) return false;
                    return true;
                }).WithMessage("Amenity type with this Id doesn't exist.").WithErrorCode("404");
        }
    }
}
