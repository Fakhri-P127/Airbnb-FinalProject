using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.Property;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context
{
    public class AirbnbDbContext:IdentityDbContext
    {
        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options):base(options)
        {

        }
        #region User related sets
        public DbSet<AppUser> AppUsers{ get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AppUserLanguage> AppUserLanguages { get; set; }
        #endregion
        #region Property related dbsets
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyGroup> PropertyGroups { get; set; }
        public DbSet<PropertyType> PropertTypes { get; set; }
        public DbSet<PropertyGroupType> PropertGroupTypes { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<AirCover> AirCovers { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<PrivacyType> PrivacyTypes { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //AutoUpdateCreatedAndModifiedValue();
            
            base.OnModelCreating(builder);
        }
        private void AutoUpdateCreatedAndModifiedValue()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
            e.State == EntityState.Added
            || e.State == EntityState.Modified
            || e.State == EntityState.Unchanged));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
