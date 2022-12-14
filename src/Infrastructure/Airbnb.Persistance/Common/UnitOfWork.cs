using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated.StateRelated;
using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Common.Repositories.PropertyRelated;
using Airbnb.Persistance.Common.Repositories.PropertyRelated.StateRelated;
using Airbnb.Persistance.Common.Repositories.UserRelated;
using Airbnb.Persistance.Context;
using Airbnb.Persistance.Email;

namespace Airbnb.Persistance.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbDbContext _context;

        #region repositories
        public IHostRepository HostRepository { get => new HostRepository(_context) ?? throw new NotImplementedException(); }
        public IGenderRepository GenderRepository { get => new GenderRepository(_context) ?? throw new NotImplementedException(); }
        public IRefreshTokenRepository RefreshTokenRepository { get => new RefreshTokenRepository(_context) ?? throw new NotImplementedException(); }
        public ILanguageRepository LanguageRepository { get => new LanguageRepository(_context) ?? throw new NotImplementedException(); }
        public IGuestReviewRepository GuestReviewRepository { get => new GuestReviewRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyRepository PropertyRepository { get => new PropertyRepository(_context) ?? throw new NotImplementedException(); }
        public IAirCoverRepository AirCoverRepository { get => new AirCoverRepository(_context) ?? throw new NotImplementedException(); }
        public IAmenityTypeRepository AmenityTypeRepository { get => new AmenityTypeRepository(_context) ?? throw new NotImplementedException(); }
        public IAmenityRepository AmenityRepository { get => new AmenityRepository(_context) ?? throw new NotImplementedException(); }
        public ICancellationPolicyRepository CancellationPolicyRepository { get => new CancellationPolicyRepository(_context) ?? throw new NotImplementedException(); }
        public IPrivacyTypeRepository PrivacyTypeRepository { get => new PrivacyTypeRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyGroupRepository PropertyGroupRepository { get => new PropertyGroupRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyTypeRepository PropertyTypeRepository { get => new PropertyTypeRepository(_context) ?? throw new NotImplementedException(); }
        public IReservationRepository ReservationRepository { get => new ReservationRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyReviewRepository PropertyReviewRepository { get => new PropertyReviewRepository(_context) ?? throw new NotImplementedException(); }
        public IRegionRepository RegionRepository { get => new RegionRepository(_context) ?? throw new NotImplementedException(); }
        public ICountryRepository CountryRepository { get => new CountryRepository(_context) ?? throw new NotImplementedException(); }
        public ICityRepository CityRepository { get => new CityRepository(_context) ?? throw new NotImplementedException(); }
        public IStateRepository StateRepository { get => new StateRepository(_context) ?? throw new NotImplementedException(); }
        public ISettingsRepository SettingsRepository{ get => new SettingsRepository(_context) ?? throw new NotImplementedException(); }
        #endregion
        
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
