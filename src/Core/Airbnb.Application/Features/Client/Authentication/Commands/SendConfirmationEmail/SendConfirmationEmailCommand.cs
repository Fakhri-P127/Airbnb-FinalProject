using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.SendConfirmationEmail
{
    public class SendConfirmationEmailCommand:IRequest
    {
        public string Email { get; set; }
    
    }
}
