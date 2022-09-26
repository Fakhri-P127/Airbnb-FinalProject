using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Cities.Queries.GetById
{
    public class GetCityByIdQuery:IRequest<CityResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<City, bool>> Expression { get; set; }
        public GetCityByIdQuery(Guid id, Expression<Func<City, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}

