using Airbnb.Application.Contracts.v1.Client.Property.Parameters;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetAll
{
    public class PropertyGetAllQuery:IRequest<List<GetPropertyResponse>>
    {
        public PropertyParameters Parameters { get; set; }
        
        public Expression<Func<Property, bool>> Expression { get; set; }
        public PropertyGetAllQuery(PropertyParameters parameters,Expression<Func<Property, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
