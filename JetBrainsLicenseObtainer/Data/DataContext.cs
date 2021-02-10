using JetBrainsLicenseObtainer.Models;
using Microsoft.EntityFrameworkCore;

namespace JetBrainsLicenseObtainer.Data
{
    public class DataContext : DbContext
    { 
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Key> OutdatedKeys { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=jblodb;Trusted_Connection=True;");
        }
    }
}
