using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryValidator:AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(10, 30);
            RuleFor(x => x.Password).NotEmpty().Length(8, 30);
        }
    }
}
