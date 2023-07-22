using API_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbECommerce;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Product> Products { get; set; }
    }
}
