using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Domain.Entities.AppUserRelated;

namespace Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses
{
    public class RefreshTokenResponse:BaseResponse
    {
        public string JwtId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool HasBeenUsed { get; set; }
        public bool IsRevoked { get; set; }
        public Guid AppUserId { get; set; }
    }
}
