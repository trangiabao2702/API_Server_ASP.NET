using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using API_Server.Services.EmailService;
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
        private readonly IEmailService _emailService;
        private static List<VerifyCode> _verifyCodes = new List<VerifyCode>();
        private static Dictionary<string, int> _refreshTokens = new Dictionary<string, int>();

        public AuthController(DataContext context, IConfiguration configuration, IEmailService emailService) 
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
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

        [HttpPost("verify/{id}")]
        public IActionResult Verify([FromBody] string code, int id)
        {
            VerifyCode? verifyCode = _verifyCodes.FirstOrDefault(c => c.Code == code);

            if (verifyCode != null)
            {
                bool checkExpired = verifyCode.ExpiredTime.CompareTo(DateTime.Now) < 0;
                bool checkOwner = verifyCode.Owner == id;

                _verifyCodes.Remove(verifyCode);

                if (!checkExpired && checkOwner)
                {
                    return Ok("Verified successfully!");
                }
            }

            return BadRequest("Invalid code!");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User request)
        {
            try
            {
                // Check if username existed
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email || u.Phone == request.Phone );

                if (user == null)
                {
                    // Create new user
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    user = new User();

                    user.Email = request.Email;
                    user.Phone = request.Phone;
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
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
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Username);

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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string username)
        {
            try
            {
                // Check if user exists
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == username || u.Phone == username);

                if (user == null)
                {
                    return NotFound("Username doesn't exist!");
                }
                else
                {
                    // Delete old user's verify codes
                    _verifyCodes.RemoveAll(c => c.Owner == user.Id);

                    // Send verify code to Email
                    VerifyCode verifyCode = new VerifyCode(user.Id);
                    _verifyCodes.Add(verifyCode);

                    var message = new Message
                    (
                        new List<string> { "trangiabao.zuki@gmail.com" }, 
                        "Forgot Password", 
                        verifyCode.Code
                    );
                    // var message = new Message(new List<string> { user.Email }, "Forgot Password", verifyCode.Code);
                    _emailService.SendEmail(message);

                    return Ok("We have sent verify code to your email!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password/{id}")]
        public async Task<IActionResult> ResetPassword([FromBody] string newPassword, int id)
        {
            try
            {
                User? user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound("User not found!");
                }
                else
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    user.Password = passwordHash;

                    _context.SaveChanges();

                    return Ok("Reset password successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
