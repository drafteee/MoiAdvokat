using Microsoft.EntityFrameworkCore;
using LawyerService.Entities.Lawyer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Address;
using System;
using LawyerService.Entities;
using System.Linq;

namespace LawyerService.DataAccess
{
    public class LawyerDbContext : IdentityDbContext<User, Role, string>
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
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);

            SpecifyUniqueIndicesForLawyers(modelBuilder);
            SpecifyTypesForDates(modelBuilder);
        }

        /// <summary>
        /// Определяет уникальность столбцов для сущности Lawyers
        /// </summary>
        /// <param name="builder"></param>
        private void SpecifyUniqueIndicesForLawyers(ModelBuilder builder)
        {
            builder.Entity<Lawyer>()
                .HasIndex(x => x.LicenseNumber)
                .IsUnique();
        }

        /// <summary>
        /// Определяет тип дат в сущностях, наследуемых от <see cref="BaseEntity"/>
        /// </summary>
        /// <param name="builder"></param>
        private void SpecifyTypesForDates(ModelBuilder builder)
        {
            foreach(var type in typeof(BaseEntity).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseEntity))).ToList())
            {
                builder.Entity(type).Property("CreatedOn").HasColumnType("timestamp with time zone");
                builder.Entity(type).Property("DeletedOn").HasColumnType("timestamp with time zone");
            }
        }
    }
}
