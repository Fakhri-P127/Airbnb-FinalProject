using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters
{
    public class RefreshTokenParameters:BaseQueryStringParameters
    {
        public DateTime? MinExpiryDate { get; set; }
        public DateTime? MaxExpiryDate { get; set; }
        public bool? HasBeenUsed { get; set; }
        public bool? IsRevoked { get; set; }
        public Guid? AppUserId { get; set; }

    }
}
