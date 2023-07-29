namespace API_Server.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set; }
        public Payment Payment { get; set; }
    }
}
