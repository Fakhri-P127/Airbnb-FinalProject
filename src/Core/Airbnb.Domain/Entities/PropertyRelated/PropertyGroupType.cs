using Airbnb.Domain.Entities.Base;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class PropertyGroupType : BaseEntity
    {
        public PropertyGroup PropertyGroup { get; set; }
        public Guid PropertyGroupId { get; set; }
        public PropertyType PropertyType { get; set; }
        public Guid PropertyTypeId { get; set; }

    }
}
