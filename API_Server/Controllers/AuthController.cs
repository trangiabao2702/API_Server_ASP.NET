using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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
        private static Dictionary<string, int> _refreshTokens = new Dictionary<string, int>();

        public AuthController(DataContext context, IConfiguration configuration) 
        {
            this._context = context;
            this._configuration = configuration;
        }

        [NonAction]
        private string CreateToken(int userId, string tokenType = "Access")
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId.ToString())
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
                        string accessToken = CreateToken(user.Id);
                        string refreshToken = CreateToken(user.Id, "Refresh");

                        _refreshTokens.Add(refreshToken, user.Id);

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

        [HttpPost("refresh-token")]
        public ActionResult<Tokens> RefreshToken([FromBody] string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:RefreshKey").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                // Validate Refresh Token
                var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var checkHeader = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature);

                    if (!checkHeader)
                    {
                        return Unauthorized(); 
                    }
                }

                // Check if Refresh Token was belong to the user who send request
                int userId = int.Parse(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
                if (_refreshTokens.ContainsKey(refreshToken) && _refreshTokens.ContainsValue(userId))
                {
                    // Delete old Refresh Token and generate a new one
                    _refreshTokens.Remove(refreshToken);
                }
                else
                {
                    return Forbid();
                }

                // Generate new Tokens
                string newAccessToken = CreateToken(userId);
                string newRefreshToken = CreateToken(userId, "Refresh");

                // Add new Refresh Token to list
                _refreshTokens.Add(newRefreshToken, userId);

                // Response
                var response = new
                {
                    accessToken = newAccessToken,
                    refreshToken = newRefreshToken
                };

                return Ok(response);
            }
            catch (SecurityTokenExpiredException)
            {
                // Token expired
                return Unauthorized();

            }
            catch (SecurityTokenException)
            {
                // Token is invalid
                return Unauthorized();

            }
        }
    }
}
