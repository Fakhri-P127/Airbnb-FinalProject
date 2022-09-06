using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IHostRepository HostRepository { get; }
        IAirCoverRepository AirCoverRepository { get; }
        Task SaveChangesAsync();
    }
}
