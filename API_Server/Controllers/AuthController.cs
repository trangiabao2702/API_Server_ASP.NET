using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Authorization;
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

        [NonAction]
        private KeyValuePair<string, int> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:RefreshKey").Value!))
            };

            try
            {
                // Validate Refresh Token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var checkHeader = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature);

                    if (!checkHeader)
                    {
                        return new KeyValuePair<string, int>("Header doesn't match!", 0);
                    }
                }

                // Check if Refresh Token was belong to the user who send request
                int userId = int.Parse(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
                if (_refreshTokens.ContainsKey(token) && _refreshTokens.ContainsValue(userId))
                {
                    // Delete old Refresh Token and generate a new one
                    return new KeyValuePair<string, int>("Valid token!", userId);
                }
                else
                {
                    return new KeyValuePair<string, int>("Forbidden!", 0);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return new KeyValuePair<string, int>("Token expired!", 0);
            }
            catch (SecurityTokenException)
            {
                return new KeyValuePair<string, int>("Token is invalid!", 0);
            }
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
            KeyValuePair<string, int> checkToken = ValidateToken(refreshToken);

            if (checkToken.Key == "Valid token!")
            {
                // Delete old Refresh Token
                _refreshTokens.Remove(refreshToken);

                // Generate new Tokens
                string newAccessToken = CreateToken(checkToken.Value);
                string newRefreshToken = CreateToken(checkToken.Value, "Refresh");

                // Add new Refresh Token to list
                _refreshTokens.Add(newRefreshToken, checkToken.Value);

                // Response
                var response = new
                {
                    accessToken = newAccessToken,
                    refreshToken = newRefreshToken
                };

                return Ok(response);
            }
            else if (checkToken.Key == "Forbidden!")
            {
                return Forbid();
            }
            else
            {
                return Unauthorized(checkToken.Key);
            }
        }

        [HttpDelete("logout"), Authorize]
        public IActionResult Logout([FromBody] string refreshToken)
        {
            KeyValuePair<string, int> checkToken = ValidateToken(refreshToken);

            if (checkToken.Key == "Valid token!")
            {
                // Delete old Refresh Token
                _refreshTokens.Remove(refreshToken);

                // Response
                return Ok("Logged out successfully!");
            }
            else if (checkToken.Key == "Forbidden!")
            {
                return Forbid("You haven't logged in!");
            }
            else
            {
                return Unauthorized(checkToken.Key);
            }
        }
    }
}
