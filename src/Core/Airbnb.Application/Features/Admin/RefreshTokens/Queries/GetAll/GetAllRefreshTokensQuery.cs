using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters;
using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetAll
{
    public class GetAllRefreshTokensQuery:IRequest<List<RefreshTokenResponse>>
    {
        public RefreshTokenParameters Parameters{ get; set; }
        public Expression<Func<RefreshToken,bool>> Expression { get; set; }
        public GetAllRefreshTokensQuery(RefreshTokenParameters parameters, Expression<Func<RefreshToken, bool>> expression=null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
