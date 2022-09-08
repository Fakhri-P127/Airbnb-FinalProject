using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetById
{
    public class GetByIdPropertyTypeQuery:IRequest<GetPropertyTypeResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<PropertyType, bool>> Expression { get; set; }
        public GetByIdPropertyTypeQuery(Guid id,Expression<Func<PropertyType, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
