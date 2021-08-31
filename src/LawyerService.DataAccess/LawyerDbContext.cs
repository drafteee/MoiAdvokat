using Microsoft.EntityFrameworkCore;
using LawyerService.Entities.Lawyer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Address;
using LawyerService.Entities.Transactions;
using LawyerService.Entities.Order;
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
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public virtual DbSet<TransactionReason> TransactionReasons { get; set; }
        public virtual DbSet<HistoryTransactions> HistoryTransactions { get; set; }
        public virtual DbSet<HistoryUserTransactions> HistoryUserTransactions { get; set; }
        
        public virtual DbSet<Specialization> Specializations { get; set; }
        
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<OrderResponse> OrderRespenses { get; set; }

        #region Orders

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<OrderSpecialization> OrderSpecializations { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderSpecialization>()
                .HasKey(r => new { r.OrderId, r.SpecializationId });

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderSpecializations)
                .WithOne()
                .HasForeignKey(e => e.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderSpecialization>()
                .HasOne(aur => aur.Order)
                .WithMany(aur => aur.OrderSpecializations)
                .HasForeignKey(aur => aur.OrderId);

            modelBuilder.Entity<OrderSpecialization>()
                .HasOne(aur => aur.Specialization)
                .WithMany(aur => aur.OrderSpecializations)
                .HasForeignKey(aur => aur.SpecializationId);

            modelBuilder.Entity<OrderResponse>()
                .HasIndex(p => new { p.OrderId, p.LawyerId }).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.BalanceId)
                .IsUnique();

            SpecifyUniqueIndicesForLawyers(modelBuilder);
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
    }
}
