using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Queries.GetAll
{
    public class GetAllAmenityQuery:IRequest<List<GetAmenityResponse>>
    {
        public Expression<Func<Amenity, bool>> Expression { get; set; }
        public GetAllAmenityQuery(Expression<Func<Amenity, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
