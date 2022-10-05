namespace Airbnb.Application.Contracts.v1.Base
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool? IsDisplayed { get; set; }
    }
}
