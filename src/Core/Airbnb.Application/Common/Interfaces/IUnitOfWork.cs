using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
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
        IAmenityTypeRepository AmenityTypeRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        ICancellationPolicyRepository CancellationPolicyRepository { get; }
        IPrivacyTypeRepository PrivacyTypeRepository { get; }
        Task SaveChangesAsync();
    }
}
