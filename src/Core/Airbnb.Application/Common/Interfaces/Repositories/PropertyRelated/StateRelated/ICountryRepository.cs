using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated.StateRelated
{
    public interface ICountryRepository:IGenericRepository<Country>
    {
    }
}
