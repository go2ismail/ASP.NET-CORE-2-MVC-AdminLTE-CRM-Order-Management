using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using motekarteknologi.Areas.security.Models;
using motekarteknologi.Models;
using motekarteknologi.ViewModels;

namespace motekarteknologi.Data
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

            builder.Entity<CompanyUser>()
                .HasKey(x => new { x.CompanyID, x.ApplicationUserID });
        }

        public DbSet<motekarteknologi.Models.ApplicationRole> ApplicationRole { get; set; }

        public DbSet<motekarteknologi.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<motekarteknologi.Areas.security.Models.Company> Company { get; set; }

        public DbSet<motekarteknologi.Areas.security.Models.CompanyUser> CompanyUser { get; set; }

        public DbSet<motekarteknologi.ViewModels.Module> Module { get; set; }
        
    }
}
