using Airbnb.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class RefreshToken:BaseEntity
    {
        //public Guid Token { get; set; }
        public string JwtId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool HasBeenUsed { get; set; }
        public bool IsRevoked { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
