using Microsoft.EntityFrameworkCore;
using FintechWebAPI.Models;

namespace FintechWebAPI.Data
{
    public class FintechDbContext : DbContext
    {
        public FintechDbContext(DbContextOptions<FintechDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.SourceAccount)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.SourceAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.TargetAccount)
            .WithMany()
            .HasForeignKey(t => t.TargetAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    }
}
