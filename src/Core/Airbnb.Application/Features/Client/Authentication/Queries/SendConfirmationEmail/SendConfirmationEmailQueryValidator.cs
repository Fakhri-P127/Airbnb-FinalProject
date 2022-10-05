using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Commands.SendConfirmationEmail
{
    public class SendConfirmationEmailQueryValidator : AbstractValidator<SendConfirmationEmailQuery>
    {
        public SendConfirmationEmailQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress().Length(8, 30).NotEmpty();
        }
    }
}
