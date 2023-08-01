using System.Text.Json.Serialization;

namespace API_Server.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        public List<OrderDetailInfoDto> OrderDetails { get; set; }
        public double Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
