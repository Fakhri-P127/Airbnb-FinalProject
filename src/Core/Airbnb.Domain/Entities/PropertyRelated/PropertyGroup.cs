﻿using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class PropertyGroup:BaseEntity
    {
        public PropertyGroup()
        {
            Properties = new List<Property>();
            PropertyGroupTypes = new List<PropertyGroupType>();
        }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        public List<PropertyGroupType> PropertyGroupTypes { get; set; }

    }
}
