using Airbnb.Application.Contracts.v1.Admin.Settings.Parameters;
using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Domain.Entities.Common;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Queries.GetAll
{
    public class GetAllSettingsQuery:IRequest<List<SettingsResponse>>
    {
        public SettingsParameters Parameters{ get; set; }
        public Expression<Func<Settings,bool>> Expression{ get; set; }
        public GetAllSettingsQuery(SettingsParameters parameters, Expression<Func<Settings, bool>> expression=null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
