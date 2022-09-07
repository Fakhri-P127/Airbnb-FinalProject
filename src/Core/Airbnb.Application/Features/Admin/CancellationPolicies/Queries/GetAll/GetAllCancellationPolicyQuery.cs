using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetAll
{
    public class GetAllCancellationPolicyQuery:IRequest<List<CancellationPolicyResponse>>
    {
        public Expression<Func<CancellationPolicy, bool>> Expression { get; set; }
        public GetAllCancellationPolicyQuery(Expression<Func<CancellationPolicy, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
