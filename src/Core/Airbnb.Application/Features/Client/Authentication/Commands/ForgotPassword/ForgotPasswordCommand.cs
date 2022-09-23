using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommand:IRequest
    {
        public string Email{ get; set; }
    }
}
