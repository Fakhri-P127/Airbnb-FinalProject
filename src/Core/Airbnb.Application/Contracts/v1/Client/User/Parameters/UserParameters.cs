using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.User.Parameters
{
    public class UserParameters:BaseQueryStringParameters
    {
        public override int PageSize { get; set; } = 4;
    }
}
