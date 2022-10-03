using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryValidator:AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            //RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(10, 30);
            RuleFor(x => x.PhoneNumber).NotEmpty()
               .Matches("^\\(?(\\+994)?\\)?[\\s\\-]?0?(50|51|55|70|77|12)[\\s\\-]?\\d{3}[\\s\\-]?\\d{2}[\\s\\-]?\\d{2}[\\s\\-]?$")
               .WithMessage("{PropertyName} {PropertyValue} is not in the correct format. We only accept Azerbaijan numbers");
            RuleFor(x => x.Password).NotEmpty().Length(8, 30);
        }
    }
}
