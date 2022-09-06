using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class Gender:BaseEntity
    {
        public string Name { get; set; }
        public List<AppUser> AppUsers { get; set; }

    }
}
