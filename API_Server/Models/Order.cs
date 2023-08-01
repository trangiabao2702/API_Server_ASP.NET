using System.Text.Json.Serialization;

namespace API_Server.Models
{
    public class Order
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public int? PaymentId { get; set; }

        [JsonIgnore]
        public Payment Payment { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public Order() { }

        public Order(int userId)
        {
            UserId = userId;
            OrderDetails = new List<OrderDetail>();
            PaymentId = null;
            Status = "Waiting for Payment";
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
    }
}
