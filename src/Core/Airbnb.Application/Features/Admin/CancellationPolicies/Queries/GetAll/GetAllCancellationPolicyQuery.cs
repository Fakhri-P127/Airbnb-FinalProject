using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Parameters;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetAll
{
    public class GetAllCancellationPolicyQuery:IRequest<List<CancellationPolicyResponse>>
    {
        public CancellationPolicyParameters Parameters{ get; set; }
        public Expression<Func<CancellationPolicy, bool>> Expression { get; set; }
        public GetAllCancellationPolicyQuery(CancellationPolicyParameters parameters,Expression<Func<CancellationPolicy, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
