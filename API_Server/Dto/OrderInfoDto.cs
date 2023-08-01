using API_Server.Models;

namespace API_Server.Dto
{
    public class OrderInfoDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
