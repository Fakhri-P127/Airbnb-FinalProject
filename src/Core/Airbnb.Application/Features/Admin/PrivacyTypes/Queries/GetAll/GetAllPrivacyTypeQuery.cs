using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll
{
    public class GetAllPrivacyTypeQuery:IRequest<List<PrivacyTypeResponse>>
    {
        public Expression<Func<PrivacyType, bool>> Expression { get; set; }
        public GetAllPrivacyTypeQuery(Expression<Func<PrivacyType, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
