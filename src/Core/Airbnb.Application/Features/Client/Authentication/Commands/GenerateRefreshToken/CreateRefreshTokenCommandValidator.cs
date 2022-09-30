using FluentValidation;

namespace Airbnb.Application.Features.Client.Authentication.Commands.GenerateRefreshToken
{
    public class CreateRefreshTokenCommandValidator:AbstractValidator<CreateRefreshTokenCommand>
    {
        public CreateRefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
