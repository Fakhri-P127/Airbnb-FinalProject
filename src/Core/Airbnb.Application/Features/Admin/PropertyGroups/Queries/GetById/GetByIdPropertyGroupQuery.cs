using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetById
{
    public class GetByIdPropertyGroupQuery:IRequest<GetPropertyGroupResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<PropertyGroup, bool>> Expression { get; set; }
        public GetByIdPropertyGroupQuery(Guid id, Expression<Func<PropertyGroup, bool>> expression = null)
        {
            Id = id;
            Expression = expression;

        }
    }
}
