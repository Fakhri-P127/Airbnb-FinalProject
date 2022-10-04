using FluentValidation;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Update
{
    public class UpdateSettingsCommandValidator:AbstractValidator<UpdateSettingsCommand>
    {
        public UpdateSettingsCommandValidator()
        {
            RuleFor(x => x.Key).Length(2, 50);
            RuleFor(x => x.Value).MaximumLength(500);
        }
    }
}
