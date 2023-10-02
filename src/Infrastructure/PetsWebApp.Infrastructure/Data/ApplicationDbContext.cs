using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetsWebApp.Core.Entities;

namespace PetsWebApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RequestAdoption> RequestAdoption { get; set; }
    }
}