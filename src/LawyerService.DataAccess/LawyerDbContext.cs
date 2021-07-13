using Microsoft.EntityFrameworkCore;
using LawyerService.Entities;

namespace LawyerService.DataAccess
{
    public class LawyerDbContext : DbContext
    {
        public static string SchemaName = "dbo";

        public LawyerDbContext(DbContextOptions<LawyerDbContext> options) : base(options) { }

        public virtual DbSet<Lawyer> Lawyers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
