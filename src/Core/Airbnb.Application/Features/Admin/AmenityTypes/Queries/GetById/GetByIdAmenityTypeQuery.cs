using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetById
{
    public class GetByIdAmenityTypeQuery:IRequest<AmenityTypeResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<AmenityType, bool>> Expression { get; set; }
        public GetByIdAmenityTypeQuery(Guid id,Expression<Func<AmenityType, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
