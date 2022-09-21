using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Regions.Queries.GetById
{
    public class GetRegionByIdQuery:IRequest<RegionResponse>
    {
        public Expression<Func<Region, bool>> Expression { get; set; }
        public GetRegionByIdQuery(Expression<Func<Region, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
