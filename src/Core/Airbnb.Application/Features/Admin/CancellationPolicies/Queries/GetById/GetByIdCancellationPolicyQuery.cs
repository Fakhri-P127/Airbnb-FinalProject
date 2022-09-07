using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetById
{
    public class GetByIdCancellationPolicyQuery:IRequest<CancellationPolicyResponse>
    {
        public Guid Id { get; set; }
        public GetByIdCancellationPolicyQuery(Guid id)
        {
            Id = id;
        }
        public Expression<Func<CancellationPolicy, bool>> Expression { get; set; }
        public GetByIdCancellationPolicyQuery(Expression<Func<CancellationPolicy, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
