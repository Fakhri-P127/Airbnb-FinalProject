using FluentValidation;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Update
{
    public class UpdateCityCommandValidator:AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 30).NotEmpty();
        }
    }
}
