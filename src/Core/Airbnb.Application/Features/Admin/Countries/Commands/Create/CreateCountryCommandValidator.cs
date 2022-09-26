using FluentValidation;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Create
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 25).NotEmpty();
            RuleFor(x => x.RegionId).NotEmpty();
        }
    }
}
