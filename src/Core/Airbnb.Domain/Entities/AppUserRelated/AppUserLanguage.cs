using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class AppUserLanguage:BaseEntity
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Language Language { get; set; }
        public Guid LanguageId { get; set; }
        
    }
}
