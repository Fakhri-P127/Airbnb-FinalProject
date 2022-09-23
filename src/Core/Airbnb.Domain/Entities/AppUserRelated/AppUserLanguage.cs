using Airbnb.Domain.Entities.Base;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class AppUserLanguage:BaseEntity
    {
        public AppUser AppUser { get; set; }
        public Guid AppUserId { get; set; }
        public Language Language { get; set; }
        public Guid LanguageId { get; set; }
        
    }
}
