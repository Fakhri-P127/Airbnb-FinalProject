using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetById
{
    public class GetHostByIdQuery:IRequest<GetHostResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<Host, bool>> Expression { get; set; }
        public GetHostByIdQuery(Guid id,Expression<Func<Host, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }

    }
}
