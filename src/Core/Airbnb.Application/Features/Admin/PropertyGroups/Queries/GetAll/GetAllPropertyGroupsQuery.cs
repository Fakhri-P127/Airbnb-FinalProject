using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetAll
{
    public class GetAllPropertyGroupsQuery:IRequest<List<GetPropertyGroupResponse>>
    {
        public Expression<Func<PropertyGroup, bool>> Expression { get; set; }
        public GetAllPropertyGroupsQuery(Expression<Func<PropertyGroup, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
