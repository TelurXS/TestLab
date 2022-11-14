using Microsoft.EntityFrameworkCore;
using TestLab.Entities;
using TestLab.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestLab.DataBase
{
    public class TestLabContext : DbContext
    {
        public DbSet<Navigation> Navigations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Data Source = {Config.DataBase.DataSource}; Initial Catalog = {Config.DataBase.InitialCatalog}; Integrated Security = True; MultipleActiveResultSets = True; TrustServerCertificate = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
