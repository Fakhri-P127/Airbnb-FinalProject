using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetAll
{
    public class GetAllAmenityTypesQuery:IRequest<List<AmenityTypeResponse>>
    {
        public AmenityTypeParameters Parameters { get; set; }
        public Expression<Func<AmenityType, bool>> Expression { get; set; }
        public GetAllAmenityTypesQuery(AmenityTypeParameters parameters,Expression<Func<AmenityType, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
