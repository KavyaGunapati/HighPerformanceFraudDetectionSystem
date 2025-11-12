using HighPerformanceFraudDetectionSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Data
{
    public class AppDbContext : DbContext
    {
      public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FraudRule> FraudRules { get; set; }
        public DbSet<FraudCase> FraudCases { get; set; }
        public DbSet<TransactionFraudRule> TransactionFraudRules { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FraudDetectionSystemDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .HasIndex(e => e.Email)
                .IsUnique();
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Customer)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FraudCase>()
                .HasOne(f=>f.Transaction)
                .WithMany(t=>t.FraudCases)
                .HasForeignKey(f=>f.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FraudCase>()
                .HasOne(f=>f.FraudRule)
                .WithMany(f=>f.FraudCases)
                .HasForeignKey(f=>f.FraudRuleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TransactionFraudRule>()
                .HasKey(tfr => new { tfr.TransactionId, tfr.FraudRuleId });
            modelBuilder.Entity<TransactionFraudRule>()
                .HasOne(t=>t.FraudRule)
                .WithMany(f=>f.TransactionFraudRules)
                .HasForeignKey(t=>t.FraudRuleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TransactionFraudRule>()
                .HasOne(t => t.Transaction)
                .WithMany(t => t.TransactionFraudRules)
                .HasForeignKey(t => t.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2)
                .IsRequired();
            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionDate)
                .IsRequired();
            modelBuilder.Entity<Transaction>()
            .Property(t => t.Status)
            .HasConversion<string>();
            modelBuilder.Entity<FraudCase>()
                .Property(t=>t.Status)
                .HasConversion<string>();
        }

    }
}
