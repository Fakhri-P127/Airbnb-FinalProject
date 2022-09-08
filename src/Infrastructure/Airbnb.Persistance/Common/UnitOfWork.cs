using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
using Airbnb.Persistance.Common.Repositories.PropertyRelated;
using Airbnb.Persistance.Common.Repositories.UserRelated;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbDbContext _context;
 
        public IUserRepository UserRepository { get => new UserRepository(_context) ?? throw new NotImplementedException(); }
        public IHostRepository HostRepository { get => new HostRepository(_context) ?? throw new NotImplementedException(); }
        public IGenderRepository GenderRepository { get => new GenderRepository(_context) ?? throw new NotImplementedException(); }
        public ILanguageRepository LanguageRepository { get => new LanguageRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyRepository PropertyRepository { get => new PropertyRepository(_context) ?? throw new NotImplementedException(); }
        public IAirCoverRepository AirCoverRepository { get => new AirCoverRepository(_context) ?? throw new NotImplementedException(); }
        public IAmenityTypeRepository AmenityTypeRepository { get => new AmenityTypeRepository(_context) ?? throw new NotImplementedException(); }
        public IAmenityRepository AmenityRepository { get => new AmenityRepository(_context) ?? throw new NotImplementedException(); }
        public ICancellationPolicyRepository CancellationPolicyRepository { get => new CancellationPolicyRepository(_context) ?? throw new NotImplementedException(); }
        public IPrivacyTypeRepository PrivacyTypeRepository { get => new PrivacyTypeRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyGroupRepository PropertyGroupRepository { get => new PropertyGroupRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyTypeRepository PropertyTypeRepository { get => new PropertyTypeRepository(_context) ?? throw new NotImplementedException(); }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public UnitOfWork(AirbnbDbContext context)
        {
            _context = context;
        }
    }
}
