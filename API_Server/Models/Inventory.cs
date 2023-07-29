namespace API_Server.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public List<Product> Products { get; set; }
    }
}
