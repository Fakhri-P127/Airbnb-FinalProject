using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetAll
{
    public class GetAllHostQuery:IRequest<List<GetHostResponse>>
    {
        public Expression<Func<Host, bool>> Expression { get; set; }
        public GetAllHostQuery(Expression<Func<Host,bool>> expression = null)
        {
            Expression = expression;
        }

    }
}
