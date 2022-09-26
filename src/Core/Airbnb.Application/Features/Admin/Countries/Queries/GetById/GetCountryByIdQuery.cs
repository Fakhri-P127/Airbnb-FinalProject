using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Countries.Queries.GetById
{
    public class GetCountryByIdQuery:IRequest<CountryResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<Country, bool>> Expression { get; set; }
        public GetCountryByIdQuery(Guid id, Expression<Func<Country, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
