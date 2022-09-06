using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Queries.GetAll
{
    public class AirCoverGetAllQuery:IRequest<List<AirCoverResponse>>
    {
        public Expression<Func<AirCover, bool>> Expression { get; set; }
        public AirCoverGetAllQuery(Expression<Func<AirCover, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
