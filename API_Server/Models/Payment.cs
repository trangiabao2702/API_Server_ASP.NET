using System.Text.Json.Serialization;

namespace API_Server.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Gateway { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
