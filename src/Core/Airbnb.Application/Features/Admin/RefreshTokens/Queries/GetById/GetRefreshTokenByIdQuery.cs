using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters;
using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetById
{
    public class GetRefreshTokenByIdQuery:IRequest<RefreshTokenResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<RefreshToken, bool>> Expression { get; set; }
        public GetRefreshTokenByIdQuery(Guid id, Expression<Func<RefreshToken, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
