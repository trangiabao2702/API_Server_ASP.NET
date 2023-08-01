using API_Server.Models;

namespace API_Server.Dto
{
    public class ProductSearchDto
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
