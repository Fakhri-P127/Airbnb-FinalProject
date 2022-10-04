using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Domain.Entities.Common;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Queries.GetById
{
    public class GetSettingByIdQuery:IRequest<SettingsResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<Settings,bool>> Expression{ get; set; }
        public GetSettingByIdQuery(Guid id,Expression<Func<Settings,bool>> expression)
        {
            Id = id;
            Expression = expression;
        }
    }
}
