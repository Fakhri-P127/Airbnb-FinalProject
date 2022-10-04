using FluentValidation;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Create
{
    public class CreatePropertyTypeCommandValidator:AbstractValidator<CreatePropertyTypeCommand>
    {
        public CreatePropertyTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 60);
            RuleFor(x => x.Description).Length(3,300);
            RuleFor(x => x.Icon).NotEmpty().MaximumLength(200);
        }
    }
}
