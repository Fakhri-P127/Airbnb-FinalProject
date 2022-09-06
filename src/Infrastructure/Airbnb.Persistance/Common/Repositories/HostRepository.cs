using Airbnb.Application.Common.Interfaces.Repositories;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Context;
using Airbnb.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Common.Repositories
{
    public class HostRepository : GenericRepository<Host>, IHostRepository
    {
        public HostRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
