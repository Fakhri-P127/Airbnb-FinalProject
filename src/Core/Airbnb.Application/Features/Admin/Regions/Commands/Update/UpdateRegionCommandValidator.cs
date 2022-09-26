using FluentValidation;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Update
{
    public class UpdateRegionCommandValidator:AbstractValidator<UpdateRegionCommand>
    {
        public UpdateRegionCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 25).NotEmpty();
        }
    }
}
