using Airbnb.Domain.Entities.Base;

namespace Airbnb.Domain.Entities.Common
{
    public class Settings:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
