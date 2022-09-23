using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Queries.ConfirmEmail
{
    public class ConfirmEmailQueryValidator:AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress().Length(8, 30).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
