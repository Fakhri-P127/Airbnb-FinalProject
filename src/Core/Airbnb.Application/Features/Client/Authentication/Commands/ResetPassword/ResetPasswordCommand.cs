using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommand:IRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
