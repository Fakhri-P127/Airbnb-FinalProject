namespace Airbnb.Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt{ get; set; }
        public bool? IsDisplayed { get; set; } 

    }
}
