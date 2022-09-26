using FluentValidation;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Update
{
    public class UpdateCountryCommandValidator:AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 25).NotEmpty();
        }
    }
}
