using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.UserRelated
{
    public class HostRepository : GenericRepository<Host>, IHostRepository
    {
        public HostRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
