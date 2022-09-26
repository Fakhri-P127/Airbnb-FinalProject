using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Cities.Queries.GetAll
{
    public class GetAllCitiesQuery:IRequest<List<CityResponse>>
    {
        public Expression<Func<City, bool>> Expression { get; set; }
        public GetAllCitiesQuery(Expression<Func<City, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
