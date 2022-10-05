using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Queries.ConfirmEmail
{
    public class ConfirmEmailCommandValidator:AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().Length(8, 30).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
