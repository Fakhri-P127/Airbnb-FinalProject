using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Queries.ConfirmEmail
{
    public class ConfirmEmailCommand:IRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
      
    }
}
