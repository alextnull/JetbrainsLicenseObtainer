using JetBrainsLicenseObtainer.Models;
using Microsoft.EntityFrameworkCore;

namespace JetBrainsLicenseObtainer.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}
