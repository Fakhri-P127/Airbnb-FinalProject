using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Common.Interfaces.Repositories;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Authentication;
using Airbnb.Persistance.Common.Repositories;
using Airbnb.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbDbContext _context;
 
      
        public IUserRepository UserRepository { get => new UserRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyRepository PropertyRepository { get => new PropertyRepository(_context) ?? throw new NotImplementedException(); }
        public IHostRepository HostRepository { get => new HostRepository(_context) ?? throw new NotImplementedException(); }
        public IAirCoverRepository AirCoverRepository { get => new AirCoverRepository(_context) ?? throw new NotImplementedException(); }
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
