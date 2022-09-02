using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.Property
{
    public class PropertyImage:BaseEntity
    {
        public string Name { get; set; }
        // alternative e ehtiyac yoxdu chunki product yaradanda alternative deyer vermirik
        //public string Alternative { get; set; }
        public bool IsMain { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
