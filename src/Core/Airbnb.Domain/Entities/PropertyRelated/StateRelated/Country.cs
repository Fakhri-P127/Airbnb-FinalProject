using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated.StateRelated
{
    public class Country:BaseEntity
    {
        public Country()
        {
            Cities = new();
            States = new();
        }
        public string Name { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public List<City> Cities { get; set; }
        public List<State> States { get; set; }

    }
}
