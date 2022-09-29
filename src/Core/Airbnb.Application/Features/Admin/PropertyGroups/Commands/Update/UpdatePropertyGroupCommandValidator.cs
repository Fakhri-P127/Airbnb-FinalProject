using FluentValidation;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update
{
    public class UpdatePropertyGroupCommandValidator:AbstractValidator<UpdatePropertyGroupCommand>
    {
        public UpdatePropertyGroupCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 50);
        }
    }
}
