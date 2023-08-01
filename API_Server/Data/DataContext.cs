using API_Server.Dto;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Shoes",
                    Description = "Shoes's Decription",
                });

            modelBuilder.Entity<Product>().HasData(
                new Product(new ProductCreateDto
                {
                    Id = 1,
                    Name = "Nike Air Zoom Pegasus 36",
                    Images = "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/air-zoom-pegasus-36-mens-running-shoe-wide-D24Mcz-removebg-preview.png']",
                    Description = "The iconic Nike Air Zoom Pegasus 36 offers more cooling and mesh that targets breathability across high-heat areas. A slimmer heel collar and tongue reduce bulk, while exposed cables give you a snug fit at higher speeds.",
                    CategoryId = 1,
                    Price = 108.97,
                }),
                new Product(new ProductCreateDto
                {
                    Id = 2,
                    Name = "Nike Air Zoom Pegasus 36 Shield",
                    Images = "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/air-zoom-pegasus-36-shield-mens-running-shoe-24FBGb__1_-removebg-preview.png']",
                    Description = "The Nike Air Zoom Pegasus 36 Shield gets updated to conquer wet routes. A water-repellent upper combines with an outsole that helps create grip on wet surfaces, letting you run in confidence despite the weather.",
                    CategoryId = 1,
                    Price = 89.97,
                }),
                new Product(new ProductCreateDto
                {
                    Id = 3,
                    Name = "Nike CruzrOne",
                    Images = "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/cruzrone-unisex-shoe-T2rRwS-removebg-preview.png']",
                    Description = "Designed for steady, easy-paced movement, the Nike CruzrOne keeps you going. Its rocker-shaped sole and plush, lightweight cushioning let you move naturally and comfortably. The padded collar is lined with soft wool, adding luxury to every step, while mesh details let your foot breathe. There’s no finish line—there’s only you, one step after the next.",
                    CategoryId = 1,
                    Price = 100.97,
                }),
                new Product(new ProductCreateDto
                {
                    Id = 4,
                    Name = "Nike Epic React Flyknit 2",
                    Images = "['https://s3-us-west-2.amazonaws.com/s.cdpn.io/1315882/epic-react-flyknit-2-mens-running-shoe-2S0Cn1-removebg-preview.png']",
                    Description = "The Nike Epic React Flyknit 2 takes a step up from its predecessor with smooth, lightweight performance and a bold look. An updated Flyknit upper conforms to your foot with a minimal, supportive design. Underfoot, durable Nike React technology defies the odds by being both soft and responsive, for comfort that lasts as long as you can run.",
                    CategoryId = 1,
                    Price = 89.97,
                })
            );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
