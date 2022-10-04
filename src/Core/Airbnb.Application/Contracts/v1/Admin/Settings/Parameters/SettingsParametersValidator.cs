using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Admin.Settings.Parameters
{
    public class SettingsParametersValidator:AbstractValidator<SettingsParameters>
    {
        public SettingsParametersValidator()
        {
            RuleFor(x => x.Key).Length(1, 50);
            RuleFor(x => x.Value).Length(1,100);

        }
    }
}
