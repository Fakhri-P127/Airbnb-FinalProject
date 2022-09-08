using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetAll
{
    public class GetAllPropertyTypesQuery:IRequest<List<GetPropertyTypeResponse>>
    {
        public Expression<Func<PropertyType, bool>> Expression { get; set; }
        public GetAllPropertyTypesQuery(Expression<Func<PropertyType, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
