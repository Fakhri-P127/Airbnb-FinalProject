using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Parameters;
using FluentValidation;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetAll
{
    public class PropertyGetAllQueryValidator:AbstractValidator<PropertyGetAllQuery>
    {
        private readonly IUnitOfWork _unit;

        public PropertyGetAllQueryValidator(IUnitOfWork unit)
        {
            _unit = unit;

            RuleFor(x => x.Parameters).SetValidator(new PropertyParametersValidator(_unit));
        }
    }
}
