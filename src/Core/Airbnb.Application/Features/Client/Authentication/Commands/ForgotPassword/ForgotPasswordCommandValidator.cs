using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().Length(8, 30).NotEmpty();
        }
    }
}
