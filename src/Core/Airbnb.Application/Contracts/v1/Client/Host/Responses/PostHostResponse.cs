using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses
{
    public class PostHostResponse:BaseResponse
    {
        // hansi usere aiddi
        public Guid AppUserId { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }

    }
}
