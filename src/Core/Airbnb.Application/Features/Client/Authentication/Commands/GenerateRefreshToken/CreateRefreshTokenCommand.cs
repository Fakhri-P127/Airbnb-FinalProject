using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.GenerateRefreshToken
{
    public class CreateRefreshTokenCommand : IRequest<AuthSuccessResponse>
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
