using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetAll
{
    public class GetAllPropertyGroupsQuery:IRequest<List<GetPropertyGroupResponse>>
    {
        public PropertyGroupParameters Parameters{ get; set; }
        public Expression<Func<PropertyGroup, bool>> Expression { get; set; }
        public GetAllPropertyGroupsQuery(PropertyGroupParameters parameters,Expression<Func<PropertyGroup, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
