using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class CancellationPolicy:BaseEntity
    {
        public CancellationPolicy()
        {
            Properties = new();
        }
        public string Name { get; set; }
        public string FullRefund { get; set; }
        public string PartialRefund { get; set; }
        public string NoRefund { get; set; }    
        public List<Property> Properties { get; set; }
    }
}
