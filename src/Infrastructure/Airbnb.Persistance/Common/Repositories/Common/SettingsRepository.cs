using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Domain.Entities.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.Common
{
    public class SettingsRepository : GenericRepository<Settings>, ISettingsRepository
    {
        public SettingsRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
