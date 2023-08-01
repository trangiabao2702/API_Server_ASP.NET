using System.ComponentModel.DataAnnotations;

namespace API_Server.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public OrderDetail() { }

        public OrderDetail(int orderId, int productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
    }
}
