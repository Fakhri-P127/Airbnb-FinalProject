using Airbnb.Domain.Entities.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces.Repositories
{
    public interface IPropertyRepository:IGenericRepository<Property>
    {
    }
}
