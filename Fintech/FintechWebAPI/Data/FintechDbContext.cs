using Microsoft.EntityFrameworkCore;
using FintechWebAPI.Models;

namespace FintechWebAPI.Data
{
    public class FintechDbContext : DbContext
    {
        // Constructor que recibe las opciones del contexto y las pasa al constructor base
        public FintechDbContext(DbContextOptions<FintechDbContext> options) : base(options) { }

        // DbSet para la entidad Account
        public DbSet<Account> Accounts { get; set; }
        
        // DbSet para la entidad Transaction
        public DbSet<Transaction> Transactions { get; set; }
        
        // Método para configurar el modelo de datos y las relaciones entre entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Transaction y Account para SourceAccount
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceAccount) // Un Transaction tiene un SourceAccount
                .WithMany(a => a.Transactions) // Un Account tiene muchas Transactions
                .HasForeignKey(t => t.SourceAccountId) // Clave foránea en Transaction
                .OnDelete(DeleteBehavior.Restrict); // Comportamiento de eliminación: Restrict

            // Configuración de la relación entre Transaction y Account para TargetAccount
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TargetAccount) // Un Transaction tiene un TargetAccount
                .WithMany() // TargetAccount no tiene una colección de Transactions
                .HasForeignKey(t => t.TargetAccountId) // Clave foránea en Transaction
                .OnDelete(DeleteBehavior.Restrict); // Comportamiento de eliminación: Restrict
        }
    }
}