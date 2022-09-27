using Airbnb.Application.Contracts.v1.Client.Host.Parameters;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetAll
{
    public class GetAllHostQuery:IRequest<List<GetHostResponse>>
    {
        public HostParameters Parameters { get; set; }
        public Expression<Func<Host, bool>> Expression { get; set; }
        public GetAllHostQuery(HostParameters parameters, Expression<Func<Host,bool>> expression = null)
        {
            Expression = expression;
            Parameters = parameters;
        }

    }
}
