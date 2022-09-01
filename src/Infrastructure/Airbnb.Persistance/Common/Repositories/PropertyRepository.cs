using Airbnb.Application.Common.Interfaces.Repositories;
using Airbnb.Domain.Entities.Property;
using Airbnb.Persistance.Context;
using Airbnb.Persistance.Repositories;

namespace Airbnb.Persistance.Common.Repositories
{
    public class PropertyRepository : GenericRepository<Property>,IPropertyRepository
    {
        public PropertyRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
