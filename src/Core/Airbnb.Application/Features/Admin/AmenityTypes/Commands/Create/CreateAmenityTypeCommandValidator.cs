using FluentValidation;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Create
{
    public class CreateAmenityTypeCommandValidator:AbstractValidator<CreateAmenityTypeCommand>
    {
        public CreateAmenityTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 100);
        }
    }
}
