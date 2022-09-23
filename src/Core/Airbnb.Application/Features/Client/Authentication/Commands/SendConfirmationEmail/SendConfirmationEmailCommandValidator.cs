using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Commands.SendConfirmationEmail
{
    public class SendConfirmationEmailCommandValidator : AbstractValidator<SendConfirmationEmailCommand>
    {
        public SendConfirmationEmailCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().Length(8, 30).NotEmpty();
        }
    }
}
