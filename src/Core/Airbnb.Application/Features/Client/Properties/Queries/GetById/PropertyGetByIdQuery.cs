using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetById
{
    public class PropertyGetByIdQuery:IRequest<GetPropertyResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<Property, bool>> Expression { get; set; }
        public PropertyGetByIdQuery(Guid id,Expression<Func<Property, bool>> expression = null)
        {
            Expression = expression;
            Id = id;
        }
    }
}
