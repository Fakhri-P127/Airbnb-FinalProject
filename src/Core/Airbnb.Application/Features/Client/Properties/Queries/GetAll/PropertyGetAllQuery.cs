using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetAll
{
    public class PropertyGetAllQuery:IRequest<List<GetPropertyResponse>>
    {
        public Expression<Func<Property, bool>> Expression { get; set; }
        public PropertyGetAllQuery(Expression<Func<Property, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
