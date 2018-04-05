using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PortalAO.Models
{
    public abstract class BaseModel : IBaseModel
    {
        public int ID { get; set; }
    }

    public class ModelsDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Kontruktor
        /// </summary>
        public ModelsDbContext() : base("DefaultConnection")//Połaczenie z danym connectionstringiem
        {

        }

        public DbSet<RateModel> RateModels { get; set; }
        public DbSet<AdvertisementModel> AdvertisementModels { get; set; }

        public DbSet<CustomUserLogin> UserLogins { get; set; }
        public DbSet<CustomUserClaim> UserClaims { get; set; }
        public DbSet<CustomUserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomUserLogin>().HasKey<int>(l => l.UserId);
            modelBuilder.Entity<CustomRole>().HasKey<int>(r => r.Id);
            modelBuilder.Entity<CustomUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<AdvertisementModel>().HasRequired(a => a.ApplicationUser).WithMany(a => a.Advertisements);
        }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ModelsDbContext>
    {
        protected override void Seed(ModelsDbContext context)
        {
            base.Seed(context);
        }
    }
}