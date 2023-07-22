namespace API_Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Product()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            DeletedAt = null;
        }
    }
}
