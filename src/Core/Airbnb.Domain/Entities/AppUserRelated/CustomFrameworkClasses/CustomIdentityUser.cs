using Microsoft.AspNetCore.Identity;

namespace Airbnb.Domain.Entities.AppUserRelated.CustomFrameworkClasses
{
    public class CustomIdentityUser : IdentityUser<Guid>
    {
        public CustomIdentityUser()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool? IsDisplayed { get; set; }
    }
}
