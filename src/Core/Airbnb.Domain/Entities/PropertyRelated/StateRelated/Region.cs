using Airbnb.Domain.Entities.Base;

namespace Airbnb.Domain.Entities.PropertyRelated.StateRelated
{
    public class Region : BaseEntity
    {
        public Region()
        {
            Countries = new();
            States = new();
        }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
        public List<State> States { get; set; }
    }
}
