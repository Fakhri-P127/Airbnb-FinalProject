using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
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
        public PrivacyTypeParameters Parameters{ get; set; }
        public Expression<Func<PrivacyType, bool>> Expression { get; set; }
        public GetAllPrivacyTypeQuery(PrivacyTypeParameters parameters,Expression<Func<PrivacyType, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
