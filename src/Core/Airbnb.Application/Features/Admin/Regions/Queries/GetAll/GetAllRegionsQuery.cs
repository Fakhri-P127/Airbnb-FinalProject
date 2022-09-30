using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Regions.Queries.GetAll
{
    public class GetAllRegionsQuery:IRequest<List<RegionResponse>>
    {
        public RegionParameters Parameters { get; set; }
        public Expression<Func<Region, bool>> Expression { get; set; }
        public GetAllRegionsQuery(RegionParameters parameters,Expression<Func<Region, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
