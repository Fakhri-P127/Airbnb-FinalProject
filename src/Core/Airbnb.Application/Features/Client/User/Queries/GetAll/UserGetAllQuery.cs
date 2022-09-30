using Airbnb.Application.Contracts.v1.Client.User.Parameters;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.User.Queries.GetAll
{
    public class UserGetAllQuery : IRequest<List<UserResponse>>
    {
        public UserParameters Parameters{ get; set; }
        public Expression<Func<AppUser, bool>> Expression { get; set; }
        public UserGetAllQuery(UserParameters arameters,Expression<Func<AppUser, bool>> expression = null)
        {
            Parameters = arameters;
            Expression = expression;
        }
    }
}
