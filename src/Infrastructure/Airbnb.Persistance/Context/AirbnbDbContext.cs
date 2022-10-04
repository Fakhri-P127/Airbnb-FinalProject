using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Airbnb.Persistance.Context
{
    public class AirbnbDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options)
        {
            
            
        }
        #region User related sets
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AppUserLanguage> AppUserLanguages { get; set; }
        public DbSet<GuestReview> GuestReviews { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion
        #region Property related dbsets
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyGroup> PropertyGroups { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<AirCover> AirCovers { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<PrivacyType> PrivacyTypes { get; set; }
        public DbSet<AmenityType> AmenityTypes { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<PropertyReview> PropertyReviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        #endregion
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ConfigureCascadeBehaviour(builder);
            base.OnModelCreating(builder);
        }

        private static void ConfigureCascadeBehaviour(ModelBuilder builder)
        {
            #region hosts
            builder.Entity<Host>().HasMany(x => x.Properties).WithOne(x => x.Host)
                .HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region reservations
            builder.Entity<Reservation>().HasOne(x => x.GuestReview).WithOne(x => x.Reservation)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Reservation>().HasOne(x => x.Host).WithMany(x => x.Reservations)
               .HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region property reviews
            builder.Entity<PropertyReview>().HasOne(x => x.Host).WithMany(x => x.ReviewsAboutYourProperty)
              .HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<PropertyReview>().HasOne(x => x.AppUser).WithMany(x => x.ReviewsByYou)
                .HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region guest reviews
            builder.Entity<GuestReview>().HasOne(x => x.Host).WithMany(x => x.ReviewsByYou)
              .HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region states
            builder.Entity<State>().HasOne(x => x.Region).WithMany(x => x.States)
              .HasForeignKey(x => x.RegionId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<State>().HasOne(x => x.Country).WithMany(x => x.States)
               .HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region property types
            builder.Entity<PropertyType>().HasOne(x => x.PropertyGroup).WithMany(x => x.PropertyTypes)
              .HasForeignKey(x => x.PropertyGroupId).OnDelete(DeleteBehavior.NoAction);
            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoUpdateCreatedAndModifiedValue();

            return base.SaveChangesAsync(cancellationToken);
        }
        private void AutoUpdateCreatedAndModifiedValue()
        {
            var entries = ChangeTracker.Entries().Where(e => (e.Entity is BaseEntity || e.Entity is AppUser)
            && (e.State == EntityState.Added
            || e.State == EntityState.Modified));
           
            if (entries.Any(x => x.Entity is BaseEntity)) UpdateDateTimesForBaseEntity(entries);
            if (entries.Any(x => x.Entity is AppUser)) UpdateDateTimesForAppUser(entries);
        }

        private static void UpdateDateTimesForBaseEntity(IEnumerable<EntityEntry> entries)
        {
            foreach (var entityEntry in entries.Where(x => x.Entity is BaseEntity))
            {
                ((BaseEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }
        }

        private static void UpdateDateTimesForAppUser(IEnumerable<EntityEntry> entries)
        {
            foreach (var entityEntry in entries.Where(x => x.Entity is AppUser))
            {
                ((AppUser)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((AppUser)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
