using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.AirCovers.Queries.GetAll
{
    public class AirCoverGetAllQuery:IRequest<List<AirCoverResponse>>
    {
        public AirCoverParameters Parameters { get; set; }
        public Expression<Func<AirCover, bool>> Expression { get; set; }
        public AirCoverGetAllQuery(AirCoverParameters parameters,Expression<Func<AirCover, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
