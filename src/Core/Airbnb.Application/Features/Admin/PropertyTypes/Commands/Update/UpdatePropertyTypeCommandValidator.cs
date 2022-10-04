using FluentValidation;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update
{
    public class UpdatePropertyTypeCommandValidator:AbstractValidator<UpdatePropertyTypeCommand>
    {
        public UpdatePropertyTypeCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 60);
            RuleFor(x => x.Description).Length(3, 300);
            RuleFor(x => x.Icon).MaximumLength(200);
        }
    }
}
