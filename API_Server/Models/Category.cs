using System.Text.Json.Serialization;

namespace API_Server.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }

        public Category() {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            DeletedAt = null;
        }
    }
}
