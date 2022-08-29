using Airbnb.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context
{
    public class AirbnbDbContext:IdentityDbContext
    {
        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options):base(options)
        {

        }

        public DbSet<AppUser> AppUsers{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
        }
    }
}
