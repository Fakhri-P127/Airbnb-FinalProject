using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.SendConfirmationEmail
{
    public class SendConfirmationEmailQuery:IRequest
    {
        public string Email { get; set; }
    
    }
}
