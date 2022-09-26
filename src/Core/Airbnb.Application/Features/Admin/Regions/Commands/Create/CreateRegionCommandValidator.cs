using FluentValidation;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Create
{
    public class CreateRegionCommandValidator:AbstractValidator<CreateRegionCommand>
    {
        public CreateRegionCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 25).NotEmpty();
        }
    }
}
