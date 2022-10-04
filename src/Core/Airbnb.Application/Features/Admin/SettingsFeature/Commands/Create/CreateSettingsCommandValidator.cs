using FluentValidation;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Create
{
    public class CreateSettingsCommandValidator:AbstractValidator<CreateSettingsCommand>
    {
        public CreateSettingsCommandValidator()
        {
            RuleFor(x => x.Key).NotEmpty().Length(2, 50);
            RuleFor(x => x.Value).NotEmpty().MaximumLength(500);
        }
    }
}
