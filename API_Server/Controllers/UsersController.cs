using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult getProfile()
        {
            string[] profile = new string[] { "A", "AB", "ABC" };

            return Ok(profile);
        }
    }
}
