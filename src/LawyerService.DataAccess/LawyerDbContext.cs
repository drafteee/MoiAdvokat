using Microsoft.EntityFrameworkCore;
using LawyerService.Entities.Lawyer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LawyerService.Entities.Identity;

namespace LawyerService.DataAccess
{
    public class LawyerDbContext : IdentityDbContext<User, Role, string>
    {
        public static string SchemaName = "dbo";

        public LawyerDbContext(DbContextOptions<LawyerDbContext> options) : base(options) { }

        public virtual DbSet<Lawyer> Lawyers { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
