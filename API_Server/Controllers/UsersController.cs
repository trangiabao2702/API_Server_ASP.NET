using API_Server.Data;
using API_Server.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Route("{id}"), Authorize]
        public async Task<ActionResult<User>> GetProfile(int id)
        {
            try
            {
                var user = await context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound("User not found!");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
