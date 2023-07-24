using Microsoft.AspNetCore.Mvc;

namespace API_Server.Dto
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
