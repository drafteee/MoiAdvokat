using Microsoft.EntityFrameworkCore;
using LawyerService.Entities.Lawyer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Address;

namespace LawyerService.DataAccess
{
    public class LawyerDbContext : IdentityDbContext<User>
    {
        public static string SchemaName = "dbo";

        public LawyerDbContext(DbContextOptions<LawyerDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Lawyer> Lawyers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<AdministrativeTerritoryType> AdministrativeTerritoryTypes { get; set; }
        public virtual DbSet<AdministrativeTerritory> AdministrativeTerritories { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<UserBalance> UserBalances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
