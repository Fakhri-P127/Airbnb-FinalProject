using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        private DateTime _minDateTime = DateTime.Now.AddYears(-100);
        private DateTime _maxDateTime = DateTime.Now.AddYears(-18);
        public RegisterCommandValidator()
        {
           
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(6,30);
            RuleFor(x => x.Firstname).NotEmpty().Length(2,15);
            RuleFor(x => x.Lastname).NotEmpty().Length(2,15);
            RuleFor(x => x.Password).NotEmpty().Length(8,30);
            RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThanOrEqualTo(_minDateTime)
                .LessThanOrEqualTo(_maxDateTime);
            RuleFor(x => x.PhoneNumber)
                .Matches("^\\(?(\\+994)?\\)?[\\s\\-]?0?(50|51|55|70|77|12)[\\s\\-]?\\d{3}[\\s\\-]?\\d{2}[\\s\\-]?\\d{2}[\\s\\-]?$")
                .WithMessage("{PropertyName} {PropertyValue} is not in the correct format. We only accept Azerbaijan numbers");
        }
    }
}
