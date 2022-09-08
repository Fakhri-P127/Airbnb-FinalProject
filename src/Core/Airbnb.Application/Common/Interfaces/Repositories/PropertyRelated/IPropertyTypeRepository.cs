using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated
{
    public interface IPropertyTypeRepository : IGenericRepository<PropertyType>
    {
    }
}
