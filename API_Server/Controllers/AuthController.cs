using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_Server.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration) 
        {
            this._context = context;
            this._configuration = configuration;
        }

        [NonAction]
        private string CreateToken(User user, string tokenType = "Access")
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:" + tokenType + "Key").Value!));

            var expireTime = _configuration.GetValue<Double>("Jwt:" + tokenType + "Exp");

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(expireTime),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User request)
        {
            try
            {
                // Check if username existed
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    // Create new user
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    user = new User();

                    user.Username = request.Username;
                    user.Password = passwordHash;

                    // Add new user to database
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    return Ok("Registered successfully!");
                }
                else
                {
                    return BadRequest("Username already exists!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Tokens>> Login(UserDto request)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    return NotFound("Username doesn't exist!");
                }
                else
                {
                    if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                    {
                        return BadRequest("Wrong password!");
                    }
                    else
                    {
                        string accessToken = CreateToken(user);
                        string refreshToken = CreateToken(user, "Refresh");

                        var response = new
                        {
                            accessToken = accessToken,
                            refreshToken = refreshToken
                        };

                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
