using API_Server.Models;
using System.Text.Json.Serialization;

namespace API_Server.Dto
{
    public class OrderDetailDto
    {
        public int Id { get; set; }

        public OrderInfoDto Order { get; set; }
        public ProductInOrderDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
