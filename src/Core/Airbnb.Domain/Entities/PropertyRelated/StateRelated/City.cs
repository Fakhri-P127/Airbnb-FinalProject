using Airbnb.Domain.Entities.Base;
using System.Security.Principal;

namespace Airbnb.Domain.Entities.PropertyRelated.StateRelated
{
    public class City:BaseEntity
    {
        public City()
        {
            States = new();
        }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public List<State> States { get; set; }

    }
}
