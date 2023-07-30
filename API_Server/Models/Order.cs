using System.Text.Json.Serialization;

namespace API_Server.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }

        [JsonIgnore]
        public Payment Payment { get; set; }
    }
}
