using Airbnb.Application.Contracts.v1.User.Responses;
using MediatR;

namespace Airbnb.Application.Features.User.Queries.GetAll
{
    public class UserGetAllQuery : IRequest<List<UserResponse>>
    {

    }
}
