using Airbnb.Application.Contracts.v1.Admin.Amenities.Parameters;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Queries.GetAll
{
    public class GetAllAmenityQueryValidator:AbstractValidator<GetAllAmenityQuery>
    {
        public GetAllAmenityQueryValidator()
        {
            RuleFor(x => x.Parameters).SetValidator(new AmenityParametersValidator());
        }
    }
}
