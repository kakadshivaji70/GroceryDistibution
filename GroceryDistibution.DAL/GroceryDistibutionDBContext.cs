using GroceryDistibution.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GroceryDistibution.DAL
{
    public class GroceryDistibutionDbContext : DbContext
    {
        public GroceryDistibutionDbContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ApplicationRelease> ApplicationReleases { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
