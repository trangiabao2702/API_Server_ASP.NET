using API_Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Server.Dto
{
    public class ProductInOrderDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public List<Discount> Discount { get; set; }
    }
}
