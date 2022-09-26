using FluentValidation;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Create
{
    public class CreateCityCommandValidator:AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 30).NotEmpty();
            RuleFor(x => x.CountryId).NotEmpty();
        }
    }
}
