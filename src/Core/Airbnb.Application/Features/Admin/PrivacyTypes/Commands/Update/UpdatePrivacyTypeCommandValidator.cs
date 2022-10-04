using FluentValidation;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Update
{
    public class UpdatePrivacyTypeCommandValidator:AbstractValidator<UpdatePrivacyTypeCommand>
    {
        public UpdatePrivacyTypeCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 50);
        }
    }
}
