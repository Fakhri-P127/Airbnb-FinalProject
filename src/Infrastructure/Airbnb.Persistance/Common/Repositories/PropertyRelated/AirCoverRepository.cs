using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.PropertyRelated
{
    public class AirCoverRepository : GenericRepository<AirCover>, IAirCoverRepository
    {
        public AirCoverRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
