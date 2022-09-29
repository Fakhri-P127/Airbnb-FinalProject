using FluentValidation;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create
{
    public class CreatePropertyGroupCommandValidator:AbstractValidator<CreatePropertyGroupCommand>
    {
        public CreatePropertyGroupCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 50);
            RuleFor(x => x.Image).NotEmpty();
        }
    }
}
