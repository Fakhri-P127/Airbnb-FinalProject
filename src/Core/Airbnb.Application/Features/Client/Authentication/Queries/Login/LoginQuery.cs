using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQuery:IRequest<AuthSuccessResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
