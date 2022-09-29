using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class PropertyType:BaseEntity
    {
        public PropertyType()
        {
            // hashset ve icollection da etmek olardi
            Properties = new List<Property>();
            //PropertyGroupTypes = new List<PropertyGroupType>();
        }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Property> Properties { get; set; }
        public Guid PropertyGroupId { get; set; }
        public PropertyGroup PropertyGroup { get; set; }

        //public List<PropertyGroupType> PropertyGroupTypes { get; set; }

    }
}
