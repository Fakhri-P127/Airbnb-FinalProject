using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Countries.Queries.GetAll
{
    public class GetAllCountriesQuery:IRequest<List<CountryResponse>>
    {
        public Expression<Func<Country, bool>> Expression { get; set; }
        public GetAllCountriesQuery(Expression<Func<Country, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
