using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Admin.Amenities.Parameters
{
    public class AmenityParametersValidator:AbstractValidator<AmenityParameters>
    {
        public AmenityParametersValidator()
        {
            RuleFor(x => x.Name).Length(2, 60);
            RuleFor(x => x.Description).Length(2, 300);
        }
    }
}
