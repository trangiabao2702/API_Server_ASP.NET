using API_Server.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public List<Discount> Discount { get; set; }

        public Product()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            DeletedAt = null;
        }
        public Product(ProductCreateDto product)
        {
            Id = product.Id;
            Name = product.Name;
            Images = product.Images;
            Description = product.Description;
            CategoryId = product.CategoryId;
            Price = product.Price;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            DeletedAt = null;
        }
    }
}
