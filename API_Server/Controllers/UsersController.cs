using API_Server.Data;
using API_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Server.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;

        public UsersController(DataContext dataContext) 
        {
            this.context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> getProfile()
        {
            try
            {
                var users = await context.Users.ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
