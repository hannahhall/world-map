using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorldMap.Models;

namespace WorldMap.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<WorldMap.Models.Continent> Continent { get; set; }

        public DbSet<WorldMap.Models.Country> Country { get; set; }

        public DbSet<WorldMap.Models.SubRegion> SubRegion { get; set; }

        public DbSet<WorldMap.Models.Stats> Stats { get; set; }
    }
}
