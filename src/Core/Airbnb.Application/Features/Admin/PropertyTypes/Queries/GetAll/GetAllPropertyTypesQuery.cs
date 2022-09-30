using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetAll
{
    public class GetAllPropertyTypesQuery:IRequest<List<GetPropertyTypeResponse>>
    {
        public PropertyTypeParameters Parameters{ get; set; }
        public Expression<Func<PropertyType, bool>> Expression { get; set; }
        public GetAllPropertyTypesQuery(PropertyTypeParameters parameters,Expression<Func<PropertyType, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
