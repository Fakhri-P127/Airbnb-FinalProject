using Airbnb.Application.Contracts.v1.Admin.Cities.Parameters;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Cities.Queries.GetAll
{
    public class GetAllCitiesQuery:IRequest<List<CityResponse>>
    {
        public CityParameters Parameters{ get; set; }
        public Expression<Func<City, bool>> Expression { get; set; }
        public GetAllCitiesQuery(CityParameters parameters,Expression<Func<City, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
