using FluentValidation;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create
{
    public class CreatePrivacyTypeCommandValidator:AbstractValidator<CreatePrivacyTypeCommand>
    {
        public CreatePrivacyTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 50);
        }
    }
}
