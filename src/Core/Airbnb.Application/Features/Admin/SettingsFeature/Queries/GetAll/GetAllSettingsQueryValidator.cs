using Airbnb.Application.Contracts.v1.Admin.Settings.Parameters;
using FluentValidation;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Queries.GetAll
{
    public class GetAllSettingsQueryValidator:AbstractValidator<GetAllSettingsQuery>
    {
        public GetAllSettingsQueryValidator()
        {
            RuleFor(x => x.Parameters).SetValidator(new SettingsParametersValidator());
        }
    }
}
