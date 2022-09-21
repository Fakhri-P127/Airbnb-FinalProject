using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated.StateRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.PropertyRelated.StateRelated
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        public RegionRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
