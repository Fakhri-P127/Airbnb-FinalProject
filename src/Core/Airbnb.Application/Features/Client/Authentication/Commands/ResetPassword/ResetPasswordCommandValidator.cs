using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Password).Length(6, 32).NotEmpty();
            RuleFor(x => x.ConfirmPassword).Length(6, 32).NotEmpty().Equal(x=>x.Password);
            RuleFor(x => x.Email).EmailAddress().Length(8, 30).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
