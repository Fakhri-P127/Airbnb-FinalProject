using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class PrivacyType:BaseEntity
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
    }
}
