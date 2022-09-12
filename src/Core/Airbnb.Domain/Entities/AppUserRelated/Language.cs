using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class Language:BaseEntity
    {
        public Language()
        {
            AppUserLanguages = new();
        }
        public string Name { get; set; }
        public List<AppUserLanguage> AppUserLanguages { get; set; }

    }
}
