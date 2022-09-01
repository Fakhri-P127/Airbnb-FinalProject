using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Common.Interfaces.Repositories;
using Airbnb.Domain.Entities.Common;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IJwtTokenGenerator JwtTokenGenerator { get => new JwtTokenGenerator(_userManager, _jwtSettings) ?? throw new NotImplementedException(); }
        public IUserRepository UserRepository { get => new UserRepository(_context) ?? throw new NotImplementedException(); }
        public IPropertyRepository PropertyRepository { get => new PropertyRepository(_context) ?? throw new NotImplementedException(); }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public UnitOfWork(AirbnbDbContext context,UserManager<AppUser> userManager,JwtSettings jwtSettings)
        {
            _context = context;
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
    }
}
