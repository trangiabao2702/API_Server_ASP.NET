using System.Text.Json.Serialization;

namespace API_Server.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public string? Password { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime ModifiedAt { get; set; }

        [JsonIgnore]
        public DateTime? VerifiedAt { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; }

        public User()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            VerifiedAt = null;
        }
    }
}
