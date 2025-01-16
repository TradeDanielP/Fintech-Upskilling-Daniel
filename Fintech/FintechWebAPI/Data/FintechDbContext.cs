using Microsoft.EntityFrameworkCore;
using FintechWebAPI.Models;

namespace FintechWebAPI.Data
{
    public class FintechDbContext : DbContext
    {
        public FintechDbContext(DbContextOptions<FintechDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
