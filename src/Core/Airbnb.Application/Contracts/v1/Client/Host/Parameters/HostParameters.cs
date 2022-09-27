using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.Host.Parameters
{
    public class HostParameters : BaseQueryStringParameters
    {
        public int? Status { get; set; }
        //public string Status { get; set; }

        public Guid? AppUserId { get; set; }
    }
}
