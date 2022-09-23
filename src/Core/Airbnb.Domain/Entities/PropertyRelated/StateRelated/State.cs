using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated.StateRelated
{
    public class State:BaseEntity
    {
        public State()
        {
            Properties = new();
        }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public Guid CountryId { get; set; }
        public Country Country{ get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public string Street { get; set; }
        public List<Property> Properties { get; set; }

    }
}
