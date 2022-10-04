using FluentValidation;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Update
{
    public class UpdateAmenityTypeCommandValidator:AbstractValidator<UpdateAmenityTypeCommand>
    {
        public UpdateAmenityTypeCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 100);
        }
    }
}
