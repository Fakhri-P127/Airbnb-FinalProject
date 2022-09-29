using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Amenities.Parameters
{
    public class AmenityParameters : BaseQueryStringParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? AmenityTypeId { get; set; }
    }
}
